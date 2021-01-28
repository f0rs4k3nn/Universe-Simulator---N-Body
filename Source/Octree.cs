using System;
using System.Drawing;
using Lattice;

namespace NBody {

    class Octree {

        /// Toleranta gruparii maselor
        private const double Tolerance = 0.5;

        // Atenuarea efectului de prastie intre particule
        private const double Epsilon = 700;

        // Minimum latime arbore. Subarborii nu sunt creati sub aceasta valoare
        private const double MinimumWidth = 1;

        // Numar particule in arbore
        public int BodyCount = 0;

        // Masa totala de particule intr-un arbore
        public double Mass = 0;

        // Array subarbori 
        private Octree[] _subtrees = null;

        // Locatie centru arbore
        private Vector _location;

        // Latimea limitelor arborelui
        private double _width = 0;

        // Centrul de masa al particulelor in arbore
        private Vector _centerOfMass = Vector.Zero;

        // Prima particula adaugata in arbore
        private Body _firstBody = null;

        // Constructie arbore in origine
        public Octree(double width) {
            _width = width;
        }

        // Constructie arbore cu locatie si latime stabilita
        public Octree(Vector location, double width)
            : this(width) {
            _location = _centerOfMass = location;
        }

        // Adaugare particula in arbore sau subarbore
        public void Add(Body body) {
            _centerOfMass = (Mass * _centerOfMass + body.Mass * body.Location) / (Mass + body.Mass);
            Mass += body.Mass;
            BodyCount++;
            if (BodyCount == 1)
                _firstBody = body;
            else {
                AddToSubtree(body);
                if (BodyCount == 2)
                    AddToSubtree(_firstBody);
            }
        }

        // Adaugare particula in subarbore bazata pe localizare spatiala
        private void AddToSubtree(Body body) {
            double subtreeWidth = _width / 2;

            // Nu creaza subarbore daca depaseste limita latimii
            if (subtreeWidth < MinimumWidth)
                return;

            if (_subtrees == null)
                _subtrees = new Octree[8];

            // Determinarea carui subarbore apartine particula
            int subtreeIndex = 0;
            for (int i = -1; i <= 1; i += 2)
                for (int j = -1; j <= 1; j += 2)
                    for (int k = -1; k <= 1; k += 2) {
                        Vector subtreeLocation = _location + (subtreeWidth / 2) * new Vector(i, j, k);

                        // Determinare pozitie particula in cadrul subarborelui
                        if (Math.Abs(subtreeLocation.X - body.Location.X) <= subtreeWidth / 2
                         && Math.Abs(subtreeLocation.Y - body.Location.Y) <= subtreeWidth / 2
                         && Math.Abs(subtreeLocation.Z - body.Location.Z) <= subtreeWidth / 2) {

                            if (_subtrees[subtreeIndex] == null)
                                _subtrees[subtreeIndex] = new Octree(subtreeLocation, subtreeWidth);
                            _subtrees[subtreeIndex].Add(body);
                            return;
                        }
                        subtreeIndex++;
                    }
        }


        // Acceleratia unei particule bazata pe proprietatile arborelui
        public void Accelerate(Body body) {
            double dx = _centerOfMass.X - body.Location.X;
            double dy = _centerOfMass.Y - body.Location.Y;
            double dz = _centerOfMass.Z - body.Location.Z;
            double dSquared = dx * dx + dy * dy + dz * dz;

            // Arborele contine o singura particula pentru a efectua acceleratia
            // SAU
            // Raportul latime/distanta se incadreaza in toleranta definita
            // => arborele este un singur corp masiv si se efectueaza acceleratia
            if ((BodyCount == 1 && body != _firstBody) || (_width * _width < Tolerance * Tolerance * dSquared)) {

                // Componenta de acceleratie in fiecare coordonata
                double distance = Math.Sqrt(dSquared + Epsilon * Epsilon);
                double normAcc = World.G * Mass / (distance * distance * distance);

                body.Acceleration.X += normAcc * dx;
                body.Acceleration.Y += normAcc * dy;
                body.Acceleration.Z += normAcc * dz;
            }

            // Accelerare in subarbori
            else if (_subtrees != null)
                foreach (Octree subtree in _subtrees)
                    if (subtree != null)
                        subtree.Accelerate(body);
        }
    }
}
