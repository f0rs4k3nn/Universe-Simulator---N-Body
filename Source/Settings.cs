using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NBody {

    public partial class Settings : Form {

        public Settings() {
            InitializeComponent();
        }

        private void SlowParticlesClick(Object sender, EventArgs e) {
            World.Instance.Generate(SystemType.SlowParticles);
        }

        private void FastParticlesClick(Object sender, EventArgs e) {
            World.Instance.Generate(SystemType.FastParticles);
        }

        private void OrbitalSystemClick(Object sender, EventArgs e) {
            World.Instance.Generate(SystemType.OrbitalSystem);
        }

        private void ParticleNumber_Click(object sender, EventArgs e)
        {
            int n;
            Int32.TryParse(InputBox.Show("Enter number of particles:", World.Instance.BodyAllocationCount.ToString()), out n);
            World.Instance.BodyAllocationCount = n;
        }

        private void ShowTracers_Click(object sender, EventArgs e)
        {
            World.Instance.DrawTracers ^= true;
            showTracers.Text = (World.Instance.DrawTracers ? "Hide" : "Show") + " Tracers";
        }     
    }
}
