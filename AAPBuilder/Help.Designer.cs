namespace AAPBuilder
{
    partial class Help
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Help));
            this.ınfluenceTheme1 = new Influence.InfluenceTheme();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.ınfluenceTheme1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ınfluenceTheme1
            // 
            this.ınfluenceTheme1.CloseButtonExitsApp = false;
            this.ınfluenceTheme1.Controls.Add(this.linkLabel1);
            this.ınfluenceTheme1.Controls.Add(this.label1);
            this.ınfluenceTheme1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ınfluenceTheme1.Font = new System.Drawing.Font("Verdana", 9F);
            this.ınfluenceTheme1.Location = new System.Drawing.Point(0, 0);
            this.ınfluenceTheme1.MinimizeButton = false;
            this.ınfluenceTheme1.Name = "ınfluenceTheme1";
            this.ınfluenceTheme1.Size = new System.Drawing.Size(518, 184);
            this.ınfluenceTheme1.TabIndex = 0;
            this.ınfluenceTheme1.Text = "Help";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.linkLabel1.ForeColor = System.Drawing.Color.White;
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(64, 16);
            this.linkLabel1.Location = new System.Drawing.Point(12, 77);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(503, 96);
            this.linkLabel1.TabIndex = 27;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = resources.GetString("linkLabel1.Text");
            this.linkLabel1.UseCompatibleTextRendering = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(472, 24);
            this.label1.TabIndex = 25;
            this.label1.Text = "These instructions may help you to solve your problem;";
            // 
            // Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(9)))), ((int)(((byte)(10)))));
            this.ClientSize = new System.Drawing.Size(518, 184);
            this.Controls.Add(this.ınfluenceTheme1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Help";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help";
            this.ınfluenceTheme1.ResumeLayout(false);
            this.ınfluenceTheme1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Influence.InfluenceTheme ınfluenceTheme1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}