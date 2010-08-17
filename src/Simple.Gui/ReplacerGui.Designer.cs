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
            this.label5 = new System.Windows.Forms.Label();
            this.AdvancedGroup = new System.Windows.Forms.GroupBox();
            this.chkPrepare = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCatalog = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSvcName = new System.Windows.Forms.TextBox();
            this.txtIISUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMore = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnDirectory = new System.Windows.Forms.Button();
            this.txtDirectory = new System.Windows.Forms.TextBox();
            this.header1 = new Simple.Gui.Header();
            this.AdvancedGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNamespace
            // 
            this.txtNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNamespace.BackColor = System.Drawing.Color.White;
            this.txtNamespace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamespace.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamespace.Location = new System.Drawing.Point(14, 148);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(542, 35);
            this.txtNamespace.TabIndex = 0;
            this.txtNamespace.Text = "Example.Project";
            this.txtNamespace.TextChanged += new System.EventHandler(this.txtNamespace_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkRed;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(366, 462);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkRed;
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(304, 462);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(56, 28);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(9, 115);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(228, 30);
            this.label5.TabIndex = 14;
            this.label5.Text = "Project namespace:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AdvancedGroup
            // 
            this.AdvancedGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AdvancedGroup.Controls.Add(this.chkPrepare);
            this.AdvancedGroup.Controls.Add(this.label6);
            this.AdvancedGroup.Controls.Add(this.label3);
            this.AdvancedGroup.Controls.Add(this.txtCatalog);
            this.AdvancedGroup.Controls.Add(this.label2);
            this.AdvancedGroup.Controls.Add(this.txtSvcName);
            this.AdvancedGroup.Controls.Add(this.txtIISUrl);
            this.AdvancedGroup.Controls.Add(this.label1);
            this.AdvancedGroup.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdvancedGroup.Location = new System.Drawing.Point(12, 269);
            this.AdvancedGroup.Name = "AdvancedGroup";
            this.AdvancedGroup.Size = new System.Drawing.Size(544, 187);
            this.AdvancedGroup.TabIndex = 15;
            this.AdvancedGroup.TabStop = false;
            // 
            // chkPrepare
            // 
            this.chkPrepare.AutoSize = true;
            this.chkPrepare.Checked = true;
            this.chkPrepare.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrepare.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrepare.Location = new System.Drawing.Point(215, 140);
            this.chkPrepare.Name = "chkPrepare";
            this.chkPrepare.Size = new System.Drawing.Size(115, 33);
            this.chkPrepare.TabIndex = 22;
            this.chkPrepare.Text = "Prepare";
            this.chkPrepare.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(6, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(195, 30);
            this.label6.TabIndex = 21;
            this.label6.Text = "Environment:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.txtCatalog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCatalog.BackColor = System.Drawing.Color.White;
            this.txtCatalog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCatalog.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCatalog.Location = new System.Drawing.Point(215, 17);
            this.txtCatalog.Name = "txtCatalog";
            this.txtCatalog.Size = new System.Drawing.Size(315, 35);
            this.txtCatalog.TabIndex = 1;
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
            // txtSvcName
            // 
            this.txtSvcName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSvcName.BackColor = System.Drawing.Color.White;
            this.txtSvcName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSvcName.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSvcName.Location = new System.Drawing.Point(215, 99);
            this.txtSvcName.Name = "txtSvcName";
            this.txtSvcName.Size = new System.Drawing.Size(315, 35);
            this.txtSvcName.TabIndex = 3;
            // 
            // txtIISUrl
            // 
            this.txtIISUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIISUrl.BackColor = System.Drawing.Color.White;
            this.txtIISUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIISUrl.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIISUrl.Location = new System.Drawing.Point(215, 58);
            this.txtIISUrl.Name = "txtIISUrl";
            this.txtIISUrl.Size = new System.Drawing.Size(315, 35);
            this.txtIISUrl.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 30);
            this.label1.TabIndex = 18;
            this.label1.Text = "http://localhost/";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnMore
            // 
            this.btnMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkRed;
            this.btnMore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMore.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMore.Location = new System.Drawing.Point(460, 462);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(98, 28);
            this.btnMore.TabIndex = 6;
            this.btnMore.Text = "More >>";
            this.btnMore.UseVisualStyleBackColor = true;
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(9, 186);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(228, 30);
            this.label4.TabIndex = 19;
            this.label4.Text = "Location:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // folderBrowser
            // 
            this.folderBrowser.Description = "Select the folder where you project will be installed.";
            // 
            // btnDirectory
            // 
            this.btnDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDirectory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkRed;
            this.btnDirectory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnDirectory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDirectory.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDirectory.Location = new System.Drawing.Point(511, 219);
            this.btnDirectory.Name = "btnDirectory";
            this.btnDirectory.Size = new System.Drawing.Size(45, 26);
            this.btnDirectory.TabIndex = 20;
            this.btnDirectory.Text = "...";
            this.btnDirectory.UseVisualStyleBackColor = true;
            this.btnDirectory.Click += new System.EventHandler(this.btnDirectory_Click);
            // 
            // txtDirectory
            // 
            this.txtDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDirectory.BackColor = System.Drawing.Color.White;
            this.txtDirectory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDirectory.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDirectory.Location = new System.Drawing.Point(14, 219);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.Size = new System.Drawing.Size(491, 26);
            this.txtDirectory.TabIndex = 22;
            this.txtDirectory.Text = "Example.Project";
            // 
            // header1
            // 
            this.header1.Dock = System.Windows.Forms.DockStyle.Top;
            this.header1.Location = new System.Drawing.Point(0, 0);
            this.header1.Name = "header1";
            this.header1.Size = new System.Drawing.Size(570, 99);
            this.header1.TabIndex = 21;
            // 
            // ReplacerGui
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(570, 502);
            this.ControlBox = false;
            this.Controls.Add(this.txtDirectory);
            this.Controls.Add(this.header1);
            this.Controls.Add(this.btnDirectory);
            this.Controls.Add(this.label4);
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
            this.AdvancedGroup.ResumeLayout(false);
            this.AdvancedGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox AdvancedGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCatalog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSvcName;
        private System.Windows.Forms.TextBox txtIISUrl;
        private System.Windows.Forms.Button btnMore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.Button btnDirectory;
        private Header header1;
        private System.Windows.Forms.CheckBox chkPrepare;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDirectory;
    }
}

