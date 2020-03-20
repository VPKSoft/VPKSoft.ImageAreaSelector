namespace TestApp
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.odImage = new System.Windows.Forms.OpenFileDialog();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenImage = new System.Windows.Forms.ToolStripMenuItem();
            this.pbUserSelectedImage = new System.Windows.Forms.PictureBox();
            this.lbUserSelectedImage = new System.Windows.Forms.Label();
            this.iasSelectArea = new VPKSoft.ImageAreaSelector.ImageAreaSelector();
            this.msMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserSelectedImage)).BeginInit();
            this.SuspendLayout();
            // 
            // odImage
            // 
            this.odImage.Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(800, 24);
            this.msMain.TabIndex = 1;
            this.msMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpenImage});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuOpenImage
            // 
            this.mnuOpenImage.Name = "mnuOpenImage";
            this.mnuOpenImage.Size = new System.Drawing.Size(139, 22);
            this.mnuOpenImage.Text = "Open image";
            this.mnuOpenImage.Click += new System.EventHandler(this.mnuOpenImage_Click);
            // 
            // pbUserSelectedImage
            // 
            this.pbUserSelectedImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbUserSelectedImage.Location = new System.Drawing.Point(585, 43);
            this.pbUserSelectedImage.Name = "pbUserSelectedImage";
            this.pbUserSelectedImage.Size = new System.Drawing.Size(203, 203);
            this.pbUserSelectedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbUserSelectedImage.TabIndex = 2;
            this.pbUserSelectedImage.TabStop = false;
            // 
            // lbUserSelectedImage
            // 
            this.lbUserSelectedImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbUserSelectedImage.AutoSize = true;
            this.lbUserSelectedImage.Location = new System.Drawing.Point(582, 27);
            this.lbUserSelectedImage.Name = "lbUserSelectedImage";
            this.lbUserSelectedImage.Size = new System.Drawing.Size(103, 13);
            this.lbUserSelectedImage.TabIndex = 3;
            this.lbUserSelectedImage.Text = "User-selected image";
            // 
            // iasSelectArea
            // 
            this.iasSelectArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iasSelectArea.AutoScroll = true;
            this.iasSelectArea.Location = new System.Drawing.Point(12, 27);
            this.iasSelectArea.MaximumSelectionSize = new System.Drawing.Size(600, 600);
            this.iasSelectArea.MinimumSelectionSize = new System.Drawing.Size(0, 0);
            this.iasSelectArea.Name = "iasSelectArea";
            this.iasSelectArea.RequireRectangle = true;
            this.iasSelectArea.SelectImage = null;
            this.iasSelectArea.SelectionBoxColor = System.Drawing.Color.Gold;
            this.iasSelectArea.Size = new System.Drawing.Size(446, 411);
            this.iasSelectArea.TabIndex = 0;
            this.iasSelectArea.SelectedImageChanged += new VPKSoft.ImageAreaSelector.ImageAreaSelector.OnSelectedImageChanged(this.iasSelectArea_SelectedImageChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbUserSelectedImage);
            this.Controls.Add(this.pbUserSelectedImage);
            this.Controls.Add(this.iasSelectArea);
            this.Controls.Add(this.msMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "FormMain";
            this.Text = "Test App for the VPKSoft.ImageAreaSelector";
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserSelectedImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VPKSoft.ImageAreaSelector.ImageAreaSelector iasSelectArea;
        private System.Windows.Forms.OpenFileDialog odImage;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenImage;
        private System.Windows.Forms.PictureBox pbUserSelectedImage;
        private System.Windows.Forms.Label lbUserSelectedImage;
    }
}

