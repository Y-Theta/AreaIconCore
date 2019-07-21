///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using YFrameworkBase;

namespace WNMP_ex.Services {
    internal class AreaIconDrawer :SingletonBase<AreaIconDrawer> {
        #region Properties
        private IntPtr _ico = IntPtr.Zero;
        #endregion

        #region Methods
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// 获取当前的托盘图标
        /// </summary>
        public Icon GetIcon( int size = 16) {
            Image bufferedimage;

            if (_ico == IntPtr.Zero)
                bufferedimage = new Bitmap(size, size, PixelFormat.Format32bppArgb);
            else
                bufferedimage = Bitmap.FromHicon(_ico);

            Graphics g = Graphics.FromImage(bufferedimage);

            ImageAttributes art;
            ColorMatrix mtr = new ColorMatrix ();

            return Icon.FromHandle(_ico);
        }
        #endregion

        #region Constructors
        #endregion
    }
}
