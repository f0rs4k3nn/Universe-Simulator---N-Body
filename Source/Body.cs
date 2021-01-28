using System;
using System.Drawing;
using Lattice;

namespace NBody {

    // Clasa particulei generate
    class Body {

        // Locatia spatiala
        public Vector Location = Vector.Zero;

        // Velocitate
        public Vector Velocity = Vector.Zero;

        // Acceleratia
        public Vector Acceleration = Vector.Zero;

        // Masa
        public double Mass;

        // Istoric locatie + dimenisune tracer
        private Vector[] _locationHistory = new Vector[20];

        // Indexul locatiei in istoric
        private int _locationHistoryIndex = 0;

        // Raza particulei
        public double Radius {
            get {
                return GetRadius(Mass);
            }
        }

        // Functie particula cu masa precizata
        public Body(double mass) {
            Mass = mass;
        }


        // Functie particula cu locatie, masa si velocitate;
        public Body(Vector location, double mass = 1e6, Vector velocity = new Vector())
            : this(mass) {
            Location = location;
            Velocity = velocity;

            for (int i = 0; i < _locationHistory.Length; i++) {
                _locationHistory[i] = location;
            }
        }

        // Update proprietati particula
        public void Update() {
            _locationHistory[_locationHistoryIndex] = Location;
            _locationHistoryIndex = ++_locationHistoryIndex % _locationHistory.Length;

            double speed = Velocity.Magnitude();
            if (speed > World.C) {
                Velocity = World.C * Velocity.Unit();
                speed = World.C;
            }

            if (speed == 0)
                Velocity += Acceleration;
            else {
                Vector parallelAcc = Vector.Projection(Acceleration, Velocity);
                Vector orthogonalAcc = Vector.Rejection(Acceleration, Velocity);
                double alpha = Math.Sqrt(1 - Math.Pow(speed / World.C, 2));
                Velocity = (Velocity + parallelAcc + alpha * orthogonalAcc) / (1 + Vector.Dot(Velocity, Acceleration) / (World.C * World.C));
            }

            Location += Velocity;
            Acceleration = Vector.Zero;
        }

        // Rotatie particula
        public void Rotate(Vector point, Vector direction, double angle) {
            Location = Location.Rotate(point, direction, angle);


            Velocity += point;
            Velocity = Velocity.Rotate(point, direction, angle);
            Velocity -= point;
            Acceleration += point;
            Acceleration = Acceleration.Rotate(point, direction, angle);
            Acceleration -= point;

            if (DrawTracers) {
                for (int i = 0; i < _locationHistory.Length; i++) {
                    _locationHistory[i] = _locationHistory[i].Rotate(point, direction, angle);
                }
            }
        }

        // Raza in functie de masa (ecuatia este inversul volumului sferei)
        public static double GetRadius(double mass) {
            return 10 * Math.Pow(3 * mass / (4 * Math.PI), 1 / 3.0) + 10;
        }

        #region Drawing

        private static Brush DrawingBrush = Brushes.White;
        
        private static Pen TracerPen = new Pen(new SolidBrush(Color.FromArgb(40, Color.White)));

        public static bool DrawTracers {
            get;
            set;
        }

        // Desenare particula
        public void Draw(Graphics g, Renderer renderer) {
            renderer.FillCircle2D(g, DrawingBrush, Location, Radius);

            if (DrawTracers) {
                for (int i = 0; i < _locationHistory.Length; i++) {
                    int j = (_locationHistoryIndex + i) % _locationHistory.Length;
                    int k = (j + 1) % _locationHistory.Length;
                    Point start = renderer.ComputePoint(_locationHistory[j]);
                    Point end = renderer.ComputePoint(_locationHistory[k]);
                    g.DrawLine(TracerPen, start, end);
                }
            }
        }

        #endregion Drawing
    }
}
