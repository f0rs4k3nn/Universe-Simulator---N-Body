using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using Lattice;

namespace NBody {
    
    class Window : Form {

        // Interval milisecunde intre frame-uri
        private const int DrawInterval = 33;
        private const double DrawFpsEasing = 0.2;
 
        // Frameuri maxime
        private const double FpsMax = 999.9;

        private const int InfoWidth = 180;

        // Instantiere model simulare
        private World _world = World.Instance;

        // Locatia curenta a mouse-ului
        private Point _mouseLocation = new Point();

        private Boolean _mouseIsDown = false;

        private Stopwatch _stopwatch = new Stopwatch();


        // Frame-uri pentru info
        private double _fps = 0;

        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Window());
        }

        public Window() {

            // Initializare winform si mouse
            InitializeComponent();
            InitializeMouseEvents();

            // Initializare desen
            Paint += Draw;

            new Thread(new ThreadStart(() => {
                while (true) {
                    Invalidate();
                    Thread.Sleep(DrawInterval);
                }
            })) {
                IsBackground = true
            }.Start();

            new Settings().Show();
        }

        private void InitializeComponent() {
            SuspendLayout();
            BackColor = Color.Black;
            ClientSize = new Size(984, 461);
            DoubleBuffered = true;
            Name = "Window";
            Text = "N-Body Simulation";
            ResumeLayout(false);
        }

        private void InitializeMouseEvents() {

            MouseDown += (sender, e) => {
                _mouseIsDown = true;
            };

            MouseUp += (sender, e) => {
                _mouseIsDown = false;
            };

            MouseMove += (sender, e) => {
                int dx = e.X - _mouseLocation.X;
                int dy = e.Y - _mouseLocation.Y;
                _mouseLocation = e.Location;

                if (_mouseIsDown)
                    RotationHelper.MouseDrag(_world.Rotate, dx, dy);
            };

            MouseWheel += (sender, e) => {
                _world.MoveCamera(e.Delta); ;
            };
        }

        // Desenarea simularii 
        private void Draw(Object sender, PaintEventArgs e) {
            try {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Desenare spatiu univers 
                g.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2);
                _world.Draw(g);
                g.ResetTransform();

                // Afisaj info text 
                using (Font font = new Font("Lucida Console", 8))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(50, Color.White))) {
                    int x = Width - InfoWidth;

                    g.DrawString(String.Format("{0,-13}{1:#0.0}", "FPS:"      , _fps)            , font, brush, x, 10);
                    g.DrawString(String.Format("{0,-13}{1}"     , "Particles:", _world.BodyCount), font, brush, x, 24);
                }

                // FPS
                _stopwatch.Stop();
                _fps += (1000.0 / _stopwatch.Elapsed.TotalMilliseconds - _fps) * DrawFpsEasing;
                _fps = Math.Min(_fps, FpsMax);
                _stopwatch.Reset();
                _stopwatch.Start();

            } catch (Exception ex) {
                Debug.WriteLine(ex);
            }
        }
    }
}
