namespace Simple.Gui
{
    partial class ReplacerGui
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReplacerGui));
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.AdvancedGroup = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCatalog = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSvcName = new System.Windows.Forms.TextBox();
            this.txtIISUrl = new System.Windows.Forms.TextBox();
            this.Version = new System.Windows.Forms.Label();
            this.btnMore = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.line1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.AdvancedGroup.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNamespace
            // 
            this.txtNamespace.BackColor = System.Drawing.Color.White;
            this.txtNamespace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamespace.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamespace.Location = new System.Drawing.Point(14, 135);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(452, 35);
            this.txtNamespace.TabIndex = 0;
            this.txtNamespace.Text = "Sample.Project";
            this.txtNamespace.TextChanged += new System.EventHandler(this.txtNamespace_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(426, 176);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 28);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(364, 176);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(56, 28);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(23, -1);
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
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(9, 102);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(228, 30);
            this.label5.TabIndex = 14;
            this.label5.Text = "Project namespace:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AdvancedGroup
            // 
            this.AdvancedGroup.Controls.Add(this.label3);
            this.AdvancedGroup.Controls.Add(this.txtCatalog);
            this.AdvancedGroup.Controls.Add(this.label2);
            this.AdvancedGroup.Controls.Add(this.label1);
            this.AdvancedGroup.Controls.Add(this.txtSvcName);
            this.AdvancedGroup.Controls.Add(this.txtIISUrl);
            this.AdvancedGroup.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdvancedGroup.Location = new System.Drawing.Point(12, 207);
            this.AdvancedGroup.Name = "AdvancedGroup";
            this.AdvancedGroup.Size = new System.Drawing.Size(501, 147);
            this.AdvancedGroup.TabIndex = 15;
            this.AdvancedGroup.TabStop = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 30);
            this.label3.TabIndex = 20;
            this.label3.Text = "Service name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCatalog
            // 
            this.txtCatalog.BackColor = System.Drawing.Color.White;
            this.txtCatalog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCatalog.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCatalog.Location = new System.Drawing.Point(215, 17);
            this.txtCatalog.Name = "txtCatalog";
            this.txtCatalog.Size = new System.Drawing.Size(272, 35);
            this.txtCatalog.TabIndex = 14;
            this.txtCatalog.Text = "SampleProject";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 30);
            this.label2.TabIndex = 19;
            this.label2.Text = "Initial Catalog =";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 30);
            this.label1.TabIndex = 18;
            this.label1.Text = "http://localhost/";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSvcName
            // 
            this.txtSvcName.BackColor = System.Drawing.Color.White;
            this.txtSvcName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSvcName.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSvcName.Location = new System.Drawing.Point(215, 99);
            this.txtSvcName.Name = "txtSvcName";
            this.txtSvcName.Size = new System.Drawing.Size(272, 35);
            this.txtSvcName.TabIndex = 16;
            this.txtSvcName.Text = "sampleprojectsvc";
            // 
            // txtIISUrl
            // 
            this.txtIISUrl.BackColor = System.Drawing.Color.White;
            this.txtIISUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIISUrl.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIISUrl.Location = new System.Drawing.Point(215, 58);
            this.txtIISUrl.Name = "txtIISUrl";
            this.txtIISUrl.Size = new System.Drawing.Size(272, 35);
            this.txtIISUrl.TabIndex = 15;
            this.txtIISUrl.Text = "sample-project";
            // 
            // Version
            // 
            this.Version.BackColor = System.Drawing.Color.Transparent;
            this.Version.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Version.ForeColor = System.Drawing.Color.White;
            this.Version.Location = new System.Drawing.Point(288, 70);
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
            // btnMore
            // 
            this.btnMore.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMore.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMore.Location = new System.Drawing.Point(472, 135);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(42, 35);
            this.btnMore.TabIndex = 1;
            this.btnMore.Text = "...";
            this.btnMore.UseVisualStyleBackColor = true;
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkRed;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.Version);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(523, 90);
            this.panel1.TabIndex = 17;
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseMove);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseDown);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseUp);
            // 
            // line1
            // 
            this.line1.BackColor = System.Drawing.Color.IndianRed;
            this.line1.Dock = System.Windows.Forms.DockStyle.Top;
            this.line1.Location = new System.Drawing.Point(0, 90);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(523, 4);
            this.line1.TabIndex = 18;
            // 
            // ReplacerGui
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(523, 378);
            this.ControlBox = false;
            this.Controls.Add(this.line1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnMore);
            this.Controls.Add(this.AdvancedGroup);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtNamespace);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ReplacerGui";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.AdvancedGroup.ResumeLayout(false);
            this.AdvancedGroup.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox AdvancedGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCatalog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSvcName;
        private System.Windows.Forms.TextBox txtIISUrl;
        private System.Windows.Forms.Label Version;
        private System.Windows.Forms.Button btnMore;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label line1;
    }
}

