namespace WowPixel
{
    partial class frmWowPixel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWowPixel));
            this.tmrFire = new System.Windows.Forms.Timer(this.components);
            this.siticoneAnimateWindow1 = new Siticone.UI.WinForms.SiticoneAnimateWindow(this.components);
            this.textBox1 = new Siticone.UI.WinForms.SiticoneRoundedTextBox();
            this.ExitButton = new Siticone.UI.WinForms.SiticoneRoundedGradientButton();
            this.OpenCFG = new Siticone.UI.WinForms.SiticoneRoundedGradientButton();
            this.RegionButton = new Siticone.UI.WinForms.SiticoneRoundedGradientButton();
            this.EnableKey = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // tmrFire
            // 
            this.tmrFire.Enabled = true;
            this.tmrFire.Tick += new System.EventHandler(this.tmrFire_Tick);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Transparent;
            this.textBox1.BorderColor = System.Drawing.Color.Wheat;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox1.DefaultText = "";
            this.textBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBox1.DisabledState.Parent = this.textBox1;
            this.textBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBox1.FillColor = System.Drawing.Color.Transparent;
            this.textBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBox1.FocusedState.Parent = this.textBox1;
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.HoveredState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textBox1.HoveredState.Parent = this.textBox1;
            this.textBox1.Location = new System.Drawing.Point(12, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '\0';
            this.textBox1.PlaceholderText = "";
            this.textBox1.ReadOnly = true;
            this.textBox1.SelectedText = "";
            this.textBox1.ShadowDecoration.Parent = this.textBox1;
            this.textBox1.Size = new System.Drawing.Size(162, 36);
            this.textBox1.TabIndex = 17;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.Color.Transparent;
            this.ExitButton.CheckedState.Parent = this.ExitButton;
            this.ExitButton.CustomImages.Parent = this.ExitButton;
            this.ExitButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ExitButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ExitButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ExitButton.ForeColor = System.Drawing.Color.White;
            this.ExitButton.HoveredState.Parent = this.ExitButton;
            this.ExitButton.Location = new System.Drawing.Point(180, 96);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.ShadowDecoration.Parent = this.ExitButton;
            this.ExitButton.Size = new System.Drawing.Size(102, 37);
            this.ExitButton.TabIndex = 18;
            this.ExitButton.Text = "Exit";
            this.ExitButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // OpenCFG
            // 
            this.OpenCFG.BackColor = System.Drawing.Color.Transparent;
            this.OpenCFG.CheckedState.Parent = this.OpenCFG;
            this.OpenCFG.CustomImages.Parent = this.OpenCFG;
            this.OpenCFG.FillColor = System.Drawing.Color.IndianRed;
            this.OpenCFG.FillColor2 = System.Drawing.Color.Chocolate;
            this.OpenCFG.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.OpenCFG.ForeColor = System.Drawing.Color.White;
            this.OpenCFG.HoveredState.Parent = this.OpenCFG;
            this.OpenCFG.Location = new System.Drawing.Point(180, 53);
            this.OpenCFG.Name = "OpenCFG";
            this.OpenCFG.ShadowDecoration.Parent = this.OpenCFG;
            this.OpenCFG.Size = new System.Drawing.Size(102, 37);
            this.OpenCFG.TabIndex = 19;
            this.OpenCFG.Text = "Telegram CFG";
            this.OpenCFG.Click += new System.EventHandler(this.button3_Click);
            // 
            // RegionButton
            // 
            this.RegionButton.BackColor = System.Drawing.Color.Transparent;
            this.RegionButton.CheckedState.Parent = this.RegionButton;
            this.RegionButton.CustomImages.Parent = this.RegionButton;
            this.RegionButton.FillColor = System.Drawing.SystemColors.HotTrack;
            this.RegionButton.FillColor2 = System.Drawing.Color.LightBlue;
            this.RegionButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RegionButton.ForeColor = System.Drawing.Color.White;
            this.RegionButton.HoveredState.Parent = this.RegionButton;
            this.RegionButton.Location = new System.Drawing.Point(180, 10);
            this.RegionButton.Name = "RegionButton";
            this.RegionButton.ShadowDecoration.Parent = this.RegionButton;
            this.RegionButton.Size = new System.Drawing.Size(102, 37);
            this.RegionButton.TabIndex = 20;
            this.RegionButton.Text = "Region";
            this.RegionButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // EnableKey
            // 
            this.EnableKey.Appearance = System.Windows.Forms.Appearance.Button;
            this.EnableKey.AutoSize = true;
            this.EnableKey.BackColor = System.Drawing.Color.GhostWhite;
            this.EnableKey.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.EnableKey.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EnableKey.Location = new System.Drawing.Point(40, 54);
            this.EnableKey.Name = "EnableKey";
            this.EnableKey.Size = new System.Drawing.Size(99, 23);
            this.EnableKey.TabIndex = 22;
            this.EnableKey.Text = "          Start          ";
            this.EnableKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.EnableKey.UseVisualStyleBackColor = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.richTextBox1.Location = new System.Drawing.Point(12, 139);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(269, 216);
            this.richTextBox1.TabIndex = 23;
            this.richTextBox1.Text = "";
            // 
            // frmWowPixel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BackgroundImage = global::WowPixel.Properties.Resources.unnamed;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(293, 367);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.EnableKey);
            this.Controls.Add(this.RegionButton);
            this.Controls.Add(this.OpenCFG);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmWowPixel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Telegram - Whisper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer tmrFire;
        private Siticone.UI.WinForms.SiticoneAnimateWindow siticoneAnimateWindow1;
        private Siticone.UI.WinForms.SiticoneRoundedTextBox textBox1;
        private Siticone.UI.WinForms.SiticoneRoundedGradientButton ExitButton;
        private Siticone.UI.WinForms.SiticoneRoundedGradientButton OpenCFG;
        private Siticone.UI.WinForms.SiticoneRoundedGradientButton RegionButton;
        private System.Windows.Forms.CheckBox EnableKey;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

