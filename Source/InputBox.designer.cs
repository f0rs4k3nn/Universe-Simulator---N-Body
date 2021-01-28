﻿namespace NBody {
    partial class InputBox {
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
            this.responseBox = new System.Windows.Forms.TextBox();
            this.button = new System.Windows.Forms.Button();
            this.promptLabel = new System.Windows.Forms.Label();
            this.whitePanel = new System.Windows.Forms.Panel();
            this.whitePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // responseBox
            // 
            this.responseBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.responseBox.Location = new System.Drawing.Point(10, 86);
            this.responseBox.Name = "responseBox";
            this.responseBox.Size = new System.Drawing.Size(171, 23);
            this.responseBox.TabIndex = 2;
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(190, 85);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(85, 25);
            this.button.TabIndex = 3;
            this.button.Text = "OK";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.ButtonClick);
            // 
            // promptLabel
            // 
            this.promptLabel.AutoSize = true;
            this.promptLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.promptLabel.Location = new System.Drawing.Point(9, 28);
            this.promptLabel.Name = "promptLabel";
            this.promptLabel.Size = new System.Drawing.Size(75, 15);
            this.promptLabel.TabIndex = 0;
            this.promptLabel.Text = "promptLabel";
            // 
            // whitePanel
            // 
            this.whitePanel.BackColor = System.Drawing.Color.White;
            this.whitePanel.Controls.Add(this.promptLabel);
            this.whitePanel.Location = new System.Drawing.Point(0, 0);
            this.whitePanel.Name = "whitePanel";
            this.whitePanel.Size = new System.Drawing.Size(284, 70);
            this.whitePanel.TabIndex = 1;
            // 
            // InputBox
            // 
            this.AcceptButton = this.button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 122);
            this.Controls.Add(this.button);
            this.Controls.Add(this.responseBox);
            this.Controls.Add(this.whitePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.whitePanel.ResumeLayout(false);
            this.whitePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox responseBox;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.Label promptLabel;
        private System.Windows.Forms.Panel whitePanel;
    }
}