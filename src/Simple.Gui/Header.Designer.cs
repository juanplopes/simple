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
            this.line1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Version = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // line1
            // 
            this.line1.BackColor = System.Drawing.Color.IndianRed;
            this.line1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.line1.Location = new System.Drawing.Point(0, 101);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(521, 4);
            this.line1.TabIndex = 20;
            this.line1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseMove);
            this.line1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseDown);
            this.line1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseUp);
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.IndianRed;
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.White;
            this.linkLabel1.Location = new System.Drawing.Point(383, 68);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(138, 18);
            this.linkLabel1.TabIndex = 17;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "by Living Consultoria";
            this.linkLabel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseMove);
            this.linkLabel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseDown);
            this.linkLabel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(22, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(215, 92);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseMove);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.DarkRed;
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.Version);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(521, 101);
            this.panel1.TabIndex = 19;
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseMove);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseDown);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseUp);
            // 
            // Version
            // 
            this.Version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Version.BackColor = System.Drawing.Color.Transparent;
            this.Version.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Version.ForeColor = System.Drawing.Color.White;
            this.Version.Location = new System.Drawing.Point(295, 48);
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
            this.Controls.Add(this.line1);
            this.Controls.Add(this.panel1);
            this.Name = "Header";
            this.Size = new System.Drawing.Size(521, 105);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label line1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Version;
    }
}
