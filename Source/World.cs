using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using Lattice;
using Threading;

namespace NBody {


    // Tipul sistemelor de generat
    enum SystemType { SlowParticles, FastParticles, OrbitalSystem };


    // Clasa de generare a universului
    class World {

        // Numarul de milisecunde intre frame-uri
        private const int FrameInterval = 33;
        private const double FpsEasing = 0.2;

        // Numarul maxim de frame-uri
        private const double FpsMax = 999.9;

        // Camera FOV
        private const double CameraFOV = 9e8;

        // Pozitia camerei pe axa Z
        private const double CameraZDefault = 1e6;

        // Viteza de accelerare pentru zoom
        private const double CameraZAcceleration = -2e-4;
        private const double CameraZEasing = 0.94;

        // Constanta gravitationala 
        public static double G = 67;

        // Viteza maxima
        public static double C = 1e4;

        // Instantarea universului
        public static World Instance {
            get {
                if (_instance == null) {
                    _instance = new World();
                }
                return _instance;
            }
        }
        private static World _instance = null;

        // Numarul de particule alocat universului
        public int BodyAllocationCount {
            get {
                return _bodies.Length;
            }
            set {
                if (_bodies.Length != value) {
                    lock (_bodyLock)
                        _bodies = new Body[value];
                }
            }
        }

        // Numarul de particule din simulare
        public int BodyCount {
            get {
                return _tree == null ? 0 : _tree.BodyCount;
            }
        }

        // Masa totala a particulelor din simulare
        public double TotalMass {
            get {
                return _tree == null ? 0 : _tree.Mass;
            }
        }

        // Frame-uri pe secunda
        public double Fps {
            get;
            private set;
        }

        // Desenare urme particule
        public Boolean DrawTracers {
            get {
                return Body.DrawTracers;
            }
            set {
                Body.DrawTracers = value;
            }
        }

        // Numarul de particule in simulare
        private Body[] _bodies = new Body[1000];

        // Body lock
        private readonly Object _bodyLock = new Object();

        // Octree-ul ce calculeaza fortele
        private Octree _tree;

        // Instantare renderer 3D
        private Renderer _renderer = new Renderer();

        // Temporizator
        private Stopwatch _stopwatch = new Stopwatch();

        // Pozitia camerei pe axa Z
        private double _cameraZ = CameraZDefault;

        // Velocitatea camerei pe axa Z
        private double _cameraZVelocity = 0;

        // Constructie univers
        private World() {

            _renderer.Camera.Z = _cameraZ;
            _renderer.FOV = CameraFOV;
 
            new Thread(new ThreadStart(() => {
                while (true)
                    Simulate();
            })) {
                IsBackground = true
            }.Start();
        }

        // Simulare
        private void Simulate() {
            lock (_bodyLock) {
                // Update particule si determinarea latimii octree-ului 
                double halfWidth = 0;
                foreach (Body body in _bodies)
                    if (body != null) {
                            body.Update();
                            halfWidth = Math.Max(Math.Abs(body.Location.X), halfWidth);
                            halfWidth = Math.Max(Math.Abs(body.Location.Y), halfWidth);
                            halfWidth = Math.Max(Math.Abs(body.Location.Z), halfWidth);
                    }
                
                // Octree-ul trebuie sa fie putin mai mare decat dublul jumatatii de latime determinate
                _tree = new Octree(2.1 * halfWidth);
                foreach (Body body in _bodies)
                    if (body != null)
                        _tree.Add(body);

                // Accelereaza particulele paralele 
                Parallel.ForEach(_bodies, body => {
                    if (body != null)
                        _tree.Accelerate(body);
                    });
                }

            // Update camera 
            _cameraZ += _cameraZVelocity * _cameraZ;
            _cameraZ = Math.Max(1, _cameraZ);
            _cameraZVelocity *= CameraZEasing;
            _renderer.Camera.Z = _cameraZ;

            // Interval de pauza intre thread-uri
            int elapsed = (int)_stopwatch.ElapsedMilliseconds;
            if (elapsed < FrameInterval)
                Thread.Sleep(FrameInterval - elapsed);

            // Update frames
            _stopwatch.Stop();
            Fps += (1000.0 / _stopwatch.Elapsed.TotalMilliseconds - Fps) * FpsEasing;
            Fps = Math.Min(Fps, FpsMax);
            _stopwatch.Reset();
            _stopwatch.Start();
        }

        // Generare sistem gravitational
        public void Generate(SystemType type) {
            lock (_bodyLock) {
                switch (type) {

                    // Particule aflate in suspensie 
                    case SystemType.SlowParticles: {
                            for (int i = 0; i < _bodies.Length; i++) {
                                double distance = PseudoRandom.Double(1e6);
                                double angle    = PseudoRandom.Double(Math.PI * 2);
                                Vector location = new Vector(Math.Cos(angle) * distance, PseudoRandom.Double(-2e5, 2e5), Math.Sin(angle) * distance);
                                double mass     = PseudoRandom.Double(1e6) + 3e4;
                                Vector velocity = PseudoRandom.Vector(5);
                                _bodies[i] = new Body(location, mass, velocity);
                            }
                        }
                        break;

                    // Particule aflate in miscare accelerata
                    case SystemType.FastParticles: {
                            for (int i = 0; i < _bodies.Length; i++) {
                                double distance = PseudoRandom.Double(1e6);
                                double angle    = PseudoRandom.Double(Math.PI * 2);
                                Vector location = new Vector(Math.Cos(angle) * distance, PseudoRandom.Double(-2e5, 2e5), Math.Sin(angle) * distance);
                                double mass     = PseudoRandom.Double(1e6) + 3e4;
                                Vector velocity = PseudoRandom.Vector(5e3);
                                _bodies[i] = new Body(location, mass, velocity);
                            }
                        }
                        break;

                    // Sistem in orbita 
                    case SystemType.OrbitalSystem: {

                            //Dimensiunea planetei
                            _bodies[0] = new Body(2e10);

                            for (int i = 1; i < _bodies.Length; i++) {
                                double distance = PseudoRandom.Double(1e6) + _bodies[0].Radius;
                                double angle    = PseudoRandom.Double(Math.PI * 2);
                                Vector location = new Vector(Math.Cos(angle) * distance, PseudoRandom.Double(-2e4, 2e4), Math.Sin(angle) * distance);
                                double mass     = PseudoRandom.Double(1e6) + 3e4;
                                double speed    = Math.Sqrt(_bodies[0].Mass * _bodies[0].Mass * G / ((_bodies[0].Mass + mass) * distance));
                                Vector velocity = Vector.Cross(location, Vector.YAxis).Unit() * speed;
                                _bodies[i] = new Body(location, mass, velocity);
                            }
                        }
                        break;
                }
            }
        }


        // Functie de rotire a universlului apeland metodele de rotire a particulelor
        // point - punct de pornire
        // direction - directia de miscare
        // angle - unghiul de rotatie
        public void Rotate(Vector point, Vector direction, double angle) {
            lock (_bodyLock) 
                Parallel.ForEach(_bodies, body => {
                    if (body != null)
                        body.Rotate(point, direction, angle);
                });
        }

        // Miscarea camerei pe axa Z 
        public void MoveCamera(int delta) {
            _cameraZVelocity += delta * CameraZAcceleration;
        }

        // Desenarea tuturor particulelor
        public void Draw(Graphics g) {
            for (int i = 0; i < _bodies.Length; i++) {
                Body body = _bodies[i];
                if (body != null)
                    body.Draw(g, _renderer);
            }
        }
    }
}
