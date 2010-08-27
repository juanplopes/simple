namespace Simple.Gui
{
    partial class Header
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Header));
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RunningAs = new System.Windows.Forms.Label();
            this.Version = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.IndianRed;
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.White;
            this.linkLabel1.Location = new System.Drawing.Point(578, 68);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(138, 18);
            this.linkLabel1.TabIndex = 17;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "by Living Consultoria";
            this.linkLabel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseMove);
            this.linkLabel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseDown);
            this.linkLabel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseUp);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkRed;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.RunningAs);
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.Version);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(716, 100);
            this.panel1.TabIndex = 19;
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseMove);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseDown);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseUp);
            // 
            // RunningAs
            // 
            this.RunningAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RunningAs.BackColor = System.Drawing.Color.Transparent;
            this.RunningAs.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RunningAs.ForeColor = System.Drawing.Color.White;
            this.RunningAs.Location = new System.Drawing.Point(490, 28);
            this.RunningAs.Margin = new System.Windows.Forms.Padding(0);
            this.RunningAs.Name = "RunningAs";
            this.RunningAs.Size = new System.Drawing.Size(226, 20);
            this.RunningAs.TabIndex = 18;
            this.RunningAs.Text = "Administrator";
            this.RunningAs.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.RunningAs.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseMove);
            this.RunningAs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseDown);
            this.RunningAs.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseUp);
            // 
            // Version
            // 
            this.Version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Version.BackColor = System.Drawing.Color.Transparent;
            this.Version.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Version.ForeColor = System.Drawing.Color.White;
            this.Version.Location = new System.Drawing.Point(490, 48);
            this.Version.Margin = new System.Windows.Forms.Padding(0);
            this.Version.Name = "Version";
            this.Version.Size = new System.Drawing.Size(226, 20);
            this.Version.TabIndex = 16;
            this.Version.Text = "version";
            this.Version.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.Version.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseMove);
            this.Version.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseDown);
            this.Version.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseUp);
            // 
            // Header
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "Header";
            this.Size = new System.Drawing.Size(716, 100);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Version;
        private System.Windows.Forms.Label RunningAs;
    }
}
