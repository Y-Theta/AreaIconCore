using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YFrameworkBase;

namespace D6_ex.Services {
    internal class AreaIconServices : SingletonBase<AreaIconServices> {
        #region Properties
        private IntPtr _ico = IntPtr.Zero;

        #endregion

        #region Methods
        public Icon StringIcon() {
            int size = 36;
            Image bufferedimage;
            if (_ico == IntPtr.Zero)
                bufferedimage = new Bitmap(size, size, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            else
                bufferedimage = Bitmap.FromHicon(_ico);

            Graphics g = Graphics.FromImage(bufferedimage);
            g.Clear(Color.FromArgb(0, 255, 255, 255));
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            Pen pen = new Pen(Color.AliceBlue, 1f);
            g.FillRectangle(pen.Brush, new RectangleF(0, 0, 24, 24));
            _ico = (bufferedimage as Bitmap).GetHicon();

            bufferedimage.Dispose();
            g.Dispose();

            return Icon.FromHandle(_ico);
        }
        #endregion

        #region Constructors
        #endregion
    }

}
