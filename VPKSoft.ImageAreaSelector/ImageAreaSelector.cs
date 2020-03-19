#region License
/*
MIT License

Copyright(c) 2020 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VPKSoft.MessageHelper;

namespace VPKSoft.ImageAreaSelector
{
    /// <summary>
    /// A Windows Forms control to select an area of an image.
    /// Implements the <see cref="System.Windows.Forms.UserControl" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public partial class ImageAreaSelector : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAreaSelector"/> class.
        /// </summary>
        public ImageAreaSelector()
        {
            InitializeComponent();
            Disposed += ImageAreaSelector_Disposed;
        }

        #region WndProc
        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message"/> to process.</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == MessageHelper.MessageHelper.WM_MOUSEWHEEL && (m.WParamLoWord() & MessageHelper.MessageHelper.MK_CONTROL) == MessageHelper.MessageHelper.MK_CONTROL)
            {
                var wheelDelta = m.WParamHiWord();

                if (wheelDelta < 0 && Zoom > 1)
                {
                    Zoom--;
                }
                else if (wheelDelta > 0)
                {
                    Zoom++;
                }

                m.Result = IntPtr.Zero;
                return;
            }
            base.WndProc(ref m);
        }
        #endregion

        #region PublicPropertiesDesign
        /// <summary>
        /// Gets or sets the image to select an area from.
        /// </summary>
        [Browsable(true)]
        [Description("The image to select an area from"), Category("Appearance")]
        public Image SelectImage
        {
            get => pnImage.BackgroundImage;
            set
            {
                pnImage.BackgroundImage = value;
                Zoom = 100; // reset the zoom..

                if (pnImage.BackgroundImage != null)
                {
                    pnImage.Size = pnImage.BackgroundImage.Size;
                    imageOriginalSize = pnImage.BackgroundImage.Size;
                }
                else
                {
                    imageOriginalSize = default;
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the selection box for the user to select an area of the image.
        /// </summary>
        [Browsable(true)]
        [Description("A color of the selection box for the user to select an area of the image"), Category("Behaviour")]
        public Color SelectionBoxColor { get => selectionPen.Color; set => selectionPen.Color = value; }

        /// <summary>
        /// Gets or sets the minimum size of the selection in the <see cref="SelectImage"/> image.
        /// </summary>
        [Browsable(true)]
        [Description("A minimum size of the selection in the SelectImage image"), Category("Behaviour")]
        public Rectangle MinimumSelectionSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum size of the selection in the <see cref="SelectImage"/> image.
        /// </summary>
        [Browsable(true)]
        [Description("A maximum size of the selection in the SelectImage image"), Category("Behaviour")]
        public Rectangle MaximumSelectionSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the selection of the image should be a rectangle area.
        /// </summary>
        [Browsable(true)]
        [Description("A value indicating whether the selection of the image should be a rectangle area"), Category("Behaviour")]
        public bool RequireRectangle { get; set; } = true;
        #endregion

        #region PublicProperties        
        /// <summary>
        /// Gets or sets the zoom percentage of the current image in the select area.
        /// </summary>
        [Browsable(false)]
        public double Zoom
        {
            get => zoom;

            set
            {
                SetZoom(value);
                zoom = value;
            }
        }
        #endregion

        #region PrivateMethods        
        /// <summary>
        /// Determines whether the given rectangle is a valid rectangle (positive non-zero values).
        /// </summary>
        /// <param name="rectangle">The rectangle to check validity for.</param>
        /// <returns><c>true</c> if given rectangle is valid; otherwise, <c>false</c>.</returns>
        private bool IsValidRectangle(Rectangle rectangle)
        {
            return rectangle.Width > 0 && rectangle.Height > 0;
        }

        /// <summary>
        /// Sets the zoom percentage of the image to a specific value.
        /// </summary>
        /// <param name="zoomPercentage">The zoom percentage.</param>
        private void SetZoom(double zoomPercentage)
        {
            zoom = zoomPercentage;
            var width = imageOriginalSize.Width * zoomPercentage / 100.0;
            var height = imageOriginalSize.Height * zoomPercentage / 100.0;

            pnImage.Size = new Size((int)width, (int)height);
        }

        /// <summary>
        /// Gets a rectangle adjusted to the current <see cref="Zoom"/> percentage value.
        /// </summary>
        /// <param name="rectangle">The rectangle to adjust.</param>
        /// <returns>An <see cref="Rectangle"/> adjusted to the current zoom percentage in pixels.</returns>
        private Rectangle ZoomAdjustRectangle(Rectangle rectangle)
        {
            var x = rectangle.X * 100.0 / zoom;
            var y = rectangle.Y * 100.0 / zoom;
            var w = rectangle.Width * 100.0 / zoom;
            var h = rectangle.Height * 100.0 / zoom;

            return new Rectangle((int)x, (int)y, (int)w, (int)h);
        }

        /// <summary>
        /// Gets a rectangle adjusted from the current <see cref="Zoom"/> percentage value.
        /// </summary>
        /// <param name="rectangle">The rectangle to adjust.</param>
        /// <returns>An <see cref="Rectangle"/> adjusted from the current zoom percentage in pixels.</returns>
        private Rectangle RectangleAdjustZoom(Rectangle rectangle)
        {
            var x = rectangle.X;
            var y = rectangle.Y;
            var w = rectangle.Width * zoom / 100.0;
            var h = rectangle.Height * zoom / 100.0;

            return new Rectangle(x, y, (int)w, (int)h);
        }


        /// <summary>
        /// Gets the user-selected image if a selection was made.
        /// </summary>
        /// <returns>An image instance if the user selected an image from the image area; <c>null</c> otherwise.</returns>
        private Image GetSelectedImage()
        {
            var zoomCoordinates = NormalCoordinates;

            if (zoomCoordinates.Width == 0 || zoomCoordinates.Height == 0) // no zero area..
            {
                return null;
            }

            if (pnImage.BackgroundImage != null)
            {
                var bitmap = new Bitmap(zoomCoordinates.Width, zoomCoordinates.Height);
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    var destRect = new Rectangle(0, 0, zoomCoordinates.Width, zoomCoordinates.Height);

                    graphics.DrawImage(pnImage.BackgroundImage, destRect, zoomCoordinates, GraphicsUnit.Pixel);
                }

                return bitmap;
            }

            return null;
        }
        #endregion

        #region PrivateFields
        private double zoom = 100;
        private Point mouseDownPoint;
        private bool mouseDownLeft;
        private bool mouseDownRight;
        private Rectangle mouseRectangle;
        private Size imageOriginalSize;
        private readonly Pen selectionPen = new Pen(Color.Blue);
        #endregion

        #region PrivateProperties        
        /// <summary>
        /// Gets the normal (non-zoomed) coordinates within the image area.
        /// </summary>
        private Rectangle NormalCoordinates => ZoomAdjustRectangle(mouseRectangle);
        #endregion

        #region InternalEvents
        private void ImageAreaSelector_Disposed(object sender, EventArgs e)
        {
            Disposed -= ImageAreaSelector_Disposed;
            selectionPen?.Dispose();
        }

        private void pnImage_Paint(object sender, PaintEventArgs e)
        {
            if (mouseDownLeft)
            {
                e.Graphics.DrawRectangle(selectionPen, mouseRectangle);
            }
        }
        #endregion

        #region MouseHandling
        private Point MouseToParent(Control control, Point location)
        {
            var newPoint = PointToClient(control.PointToScreen(location));
            return newPoint;
        }

        private void pnImage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Zoom = 100;
        }

        private void pnImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint = e.Location;
                mouseDownLeft = true;
            }

            if (e.Button == MouseButtons.Right)
            {
                pnImage.Cursor = Cursors.SizeAll;
                mouseDownPoint = MouseToParent(pnImage, e.Location);
                mouseDownRight = true;
            }
        }

        private void pnImage_MouseMove(object sender, MouseEventArgs e)
        {
            // large images tend to grow the message loop, so do break if the button has been released..
            if (e.Button == MouseButtons.None) 
            {
                return;
            }

            if (mouseDownLeft) // if the mouse is down..
            {
                var minX = Math.Min(mouseDownPoint.X, e.X);
                var minY = Math.Min(mouseDownPoint.Y, e.Y);
                var width = Math.Abs(mouseDownPoint.X - e.X);
                var height = Math.Abs(mouseDownPoint.Y - e.Y);

                var validationMade = false;

                var maxDimension = Math.Max(width, height);

                mouseRectangle = new Rectangle(minX, minY, RequireRectangle ? maxDimension : width,
                    RequireRectangle ? maxDimension : height);

                var zoomRectangle = ZoomAdjustRectangle(mouseRectangle);

                // validate the constraints..
                if (IsValidRectangle(MaximumSelectionSize) && zoomRectangle.Width > MaximumSelectionSize.Width)
                {
                    zoomRectangle.Width = MaximumSelectionSize.Width;
                    validationMade = true;
                }

                if (IsValidRectangle(MaximumSelectionSize) && zoomRectangle.Height > MaximumSelectionSize.Height)
                {
                    zoomRectangle.Height = MaximumSelectionSize.Height;
                    validationMade = true;
                }

                if (IsValidRectangle(MinimumSelectionSize) && zoomRectangle.Width < MinimumSelectionSize.Width)
                {
                    zoomRectangle.Width = MinimumSelectionSize.Width;
                    validationMade = true;
                }

                if (IsValidRectangle(MinimumSelectionSize) && zoomRectangle.Height < MinimumSelectionSize.Height)
                {
                    zoomRectangle.Height = MinimumSelectionSize.Height;
                    validationMade = true;
                }

                if (validationMade) // re-check the size if the size was manipulated..
                {
                    maxDimension = Math.Max(zoomRectangle.Height, zoomRectangle.Width);
                    zoomRectangle = new Rectangle(minX, minY, RequireRectangle ? maxDimension : zoomRectangle.Width,
                        RequireRectangle ? maxDimension : zoomRectangle.Height);
                    mouseRectangle = RectangleAdjustZoom(zoomRectangle);
                }

                pnImage.Invalidate();
            }
            else if (mouseDownRight)
            {
                var parentPoint = MouseToParent(pnImage, e.Location);
                var offsetX = Width - parentPoint.X;
                var offsetY = Height - parentPoint.Y;

                var multiplierX = 1 - offsetX / (double) Width;
                var multiplierY = 1 - offsetY / (double) Height;

                if (multiplierX < 0)
                {
                    multiplierX = 0;
                }

                if (multiplierX > 1)
                {
                    multiplierX = 1;
                }

                if (multiplierY < 0)
                {
                    multiplierY = 0;
                }

                if (multiplierY > 1)
                {
                    multiplierY = 1;
                }


                var scrollX = (int)(HorizontalScroll.Maximum * multiplierX);
                var scrollY = (int)(VerticalScroll.Maximum * multiplierY);

                HorizontalScroll.Value = scrollX;
                VerticalScroll.Value = scrollY;
            }
        }

        private void pnImage_MouseUp(object sender, MouseEventArgs e)
        {
            Application.DoEvents(); // this prevents the refresh queue for the panel..
            pnImage.Refresh();
            Application.DoEvents(); // this prevents the refresh queue for the panel..
            mouseDownLeft = false;
            mouseDownRight = false;
            if (e.Button == MouseButtons.Left)
            {
                var image = GetSelectedImage();

                if (image == null)
                {
                    return;
                }

                // raise the event 
                using (image)
                {
                    SelectedImageChanged?.Invoke(this, new ImageSelectionChangedEventArgs { SelectedImageArea = image});
                }
            }

            pnImage.Cursor = Cursors.Cross;
        }
        #endregion

        #region Events        
        /// <summary>
        /// A delegate for the <see cref="SelectedImageChanged"/> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ImageSelectionChangedEventArgs"/> instance containing the event data.</param>
        public delegate void OnSelectedImageChanged(object sender, ImageSelectionChangedEventArgs e);

        /// <summary>
        /// Occurs when user-selected image area changed.
        /// </summary>
        [Browsable(true)]
        [Description("Occurs when user-selected image area changed"), Category(nameof(ImageAreaSelector))]
        public event OnSelectedImageChanged SelectedImageChanged;
        #endregion
    }
}
