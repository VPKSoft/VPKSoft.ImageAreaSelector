namespace VPKSoft.ImageAreaSelector
{
    partial class ImageAreaSelector
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
            this.pnImage = new VPKSoft.ImageAreaSelector.PanelExtended();
            this.SuspendLayout();
            // 
            // pnImage
            // 
            this.pnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnImage.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pnImage.Location = new System.Drawing.Point(3, 3);
            this.pnImage.Margin = new System.Windows.Forms.Padding(0);
            this.pnImage.Name = "pnImage";
            this.pnImage.Size = new System.Drawing.Size(0, 0);
            this.pnImage.TabIndex = 0;
            this.pnImage.Paint += new System.Windows.Forms.PaintEventHandler(this.pnImage_Paint);
            this.pnImage.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pnImage_MouseDoubleClick);
            this.pnImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnImage_MouseDown);
            this.pnImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnImage_MouseMove);
            this.pnImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnImage_MouseUp);
            // 
            // ImageAreaSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.pnImage);
            this.Name = "ImageAreaSelector";
            this.Size = new System.Drawing.Size(379, 317);
            this.ResumeLayout(false);

        }

        #endregion

        private PanelExtended pnImage;
    }
}
