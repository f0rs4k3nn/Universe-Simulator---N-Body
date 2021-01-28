namespace NBody {
    partial class Settings {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Functions = new System.Windows.Forms.TabPage();
            this.particleNumber = new System.Windows.Forms.Button();
            this.fastParticles = new System.Windows.Forms.Button();
            this.showTracers = new System.Windows.Forms.Button();
            this.slowParticles = new System.Windows.Forms.Button();
            this.orbitalSystem = new System.Windows.Forms.Button();
            this.appControl = new System.Windows.Forms.TabControl();
            this.Functions.SuspendLayout();
            this.appControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // Functions
            // 
            this.Functions.Controls.Add(this.particleNumber);
            this.Functions.Controls.Add(this.fastParticles);
            this.Functions.Controls.Add(this.showTracers);
            this.Functions.Controls.Add(this.slowParticles);
            this.Functions.Controls.Add(this.orbitalSystem);
            this.Functions.Location = new System.Drawing.Point(4, 22);
            this.Functions.Name = "Functions";
            this.Functions.Padding = new System.Windows.Forms.Padding(3);
            this.Functions.Size = new System.Drawing.Size(330, 113);
            this.Functions.TabIndex = 0;
            this.Functions.Text = "Functions";
            this.Functions.UseVisualStyleBackColor = true;
            // 
            // particleNumber
            // 
            this.particleNumber.Location = new System.Drawing.Point(115, 39);
            this.particleNumber.Name = "particleNumber";
            this.particleNumber.Size = new System.Drawing.Size(100, 27);
            this.particleNumber.TabIndex = 11;
            this.particleNumber.Text = "Particle Number";
            this.particleNumber.UseVisualStyleBackColor = true;
            this.particleNumber.Click += new System.EventHandler(this.ParticleNumber_Click);
            // 
            // fastParticles
            // 
            this.fastParticles.Location = new System.Drawing.Point(115, 6);
            this.fastParticles.Name = "fastParticles";
            this.fastParticles.Size = new System.Drawing.Size(100, 27);
            this.fastParticles.TabIndex = 2;
            this.fastParticles.Text = "Fast Particles";
            this.fastParticles.UseVisualStyleBackColor = true;
            this.fastParticles.Click += new System.EventHandler(this.FastParticlesClick);
            // 
            // showTracers
            // 
            this.showTracers.Location = new System.Drawing.Point(9, 39);
            this.showTracers.Name = "showTracers";
            this.showTracers.Size = new System.Drawing.Size(100, 27);
            this.showTracers.TabIndex = 10;
            this.showTracers.Text = "Show Tracers";
            this.showTracers.UseVisualStyleBackColor = true;
            this.showTracers.Click += new System.EventHandler(this.ShowTracers_Click);
            // 
            // slowParticles
            // 
            this.slowParticles.Location = new System.Drawing.Point(9, 6);
            this.slowParticles.Name = "slowParticles";
            this.slowParticles.Size = new System.Drawing.Size(100, 27);
            this.slowParticles.TabIndex = 1;
            this.slowParticles.Text = "Slow Particles";
            this.slowParticles.UseVisualStyleBackColor = true;
            this.slowParticles.Click += new System.EventHandler(this.SlowParticlesClick);
            // 
            // orbitalSystem
            // 
            this.orbitalSystem.Location = new System.Drawing.Point(221, 6);
            this.orbitalSystem.Name = "orbitalSystem";
            this.orbitalSystem.Size = new System.Drawing.Size(100, 27);
            this.orbitalSystem.TabIndex = 5;
            this.orbitalSystem.Text = "Orbital System";
            this.orbitalSystem.UseVisualStyleBackColor = true;
            this.orbitalSystem.Click += new System.EventHandler(this.OrbitalSystemClick);
            // 
            // appControl
            // 
            this.appControl.Controls.Add(this.Functions);
            this.appControl.Location = new System.Drawing.Point(12, 12);
            this.appControl.Name = "appControl";
            this.appControl.SelectedIndex = 0;
            this.appControl.Size = new System.Drawing.Size(338, 139);
            this.appControl.TabIndex = 11;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 165);
            this.Controls.Add(this.appControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.Functions.ResumeLayout(false);
            this.appControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl appControl;
        private System.Windows.Forms.TabPage Functions;
        private System.Windows.Forms.Button particleNumber;
        private System.Windows.Forms.Button slowParticles;
        private System.Windows.Forms.Button fastParticles;
        private System.Windows.Forms.Button orbitalSystem;
        private System.Windows.Forms.Button showTracers;
        
    }
}