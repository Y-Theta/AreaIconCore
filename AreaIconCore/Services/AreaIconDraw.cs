using AreaIconCore.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using YFrameworkBase;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using Point = System.Drawing.Point;

namespace AreaIconCore.Services {
    /// <summary>
    /// 托盘图标绘制函数
    /// </summary>
    public class AreaIconDraw : SingletonBase<AreaIconDraw> {
        #region Properties
        private const string _numfontfamily = "FontRes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        private IntPtr _ico = IntPtr.Zero;

        private Font _lastfont;

        private float _lastsize;

        private Dictionary<string, PrivateFontCollection> _privateFontDictionary;
        public Dictionary<string, PrivateFontCollection> PrivateFontDictionary {
            private set => _privateFontDictionary = value;
            get => _privateFontDictionary;
        }
        #endregion

        #region Methods

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// 加载字体
        /// </summary>
        /// <param name="familyname">需要加载的字体名称 ttf格式</param>
        /// <param name="assemblyname">需要加载的字体的库文件名称</param>
        /// <param name="system">是否系统字体 非系统字体则在资源中查找</param>
        /// <param name="size">字体大小</param>
        public Font LoadFont(string familyname, float size = 8, string assemblyname = null, bool system = false) {
            if (system)
                return new Font(familyname, size);
            else {
                //若字典中存在，直接改变字体大小
                if (_privateFontDictionary.ContainsKey(familyname))
                    return new Font(_privateFontDictionary[familyname].Families[0], size);
                _lastsize = size;

                //若不存在，则从资源中加载
                byte[] myText = null;
                IntPtr MeAdd = IntPtr.Zero;
                PrivateFontCollection _pfc;

                //从相关程序集中加载字体
                if (!string.IsNullOrEmpty(assemblyname)) {
                    var asb = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => {
                        return a.GetName().Name.Equals(assemblyname);
                    });
                    try {
                        if (asb is null)
                            asb = AppDomain.CurrentDomain.Load(_numfontfamily);
                        //从嵌入的资源中查找
                        var strs = asb.GetManifestResourceStream(string.Format(@"{0}.{1}.ttf", assemblyname, familyname));
                        //从一般资源中查找
                        if (strs is null)
                            strs = GetResourceNames(asb, $"{familyname.ToLower()}.ttf");
                        myText = new byte[strs.Length];
                        strs.Read(myText, 0, myText.Length);
                        strs.Dispose();
                    }
                    catch (NullReferenceException) {
                            //TODO:字体不存在
                            PopupMessageManager.Instance.Message("Font Not Found!");
                    }
                }
                else {
                    //在当前程序集资源文件中搜索
                    var asb = Assembly.GetExecutingAssembly();
                    try {
                        using (var strs = asb.GetManifestResourceStream(string.Format(@"{0}.{1}.ttf", asb.GetName().Name, familyname))) {
                            myText = new byte[strs.Length];
                            strs.Read(myText, 0, myText.Length);
                        }
                    }
                    catch {
                        //TODO:字体不存在
                        PopupMessageManager.Instance.Message("Font Not Found!");
                    }
                }
                _pfc = new PrivateFontCollection();
                MeAdd = Marshal.AllocHGlobal(myText.Length);
                Marshal.Copy(myText, 0, MeAdd, myText.Length);
                _pfc.AddMemoryFont(MeAdd, myText.Length);
                _privateFontDictionary.Add(familyname, _pfc);
                _lastfont = new Font(_pfc.Families[0], size);

                return _lastfont;
            }
        }

        /// <summary>
        /// 从程序集Resource中获取资源
        /// </summary>
        public static UnmanagedMemoryStream GetResourceNames(Assembly asm, string name) {
            string resName = asm.GetName().Name + ".g.resources";
            using (var stream = asm.GetManifestResourceStream(resName))
            using (var reader = new ResourceReader(stream)) {
                return (UnmanagedMemoryStream)reader.Cast<DictionaryEntry>().FirstOrDefault(entry => { return (string)entry.Key == (name); }).Value;
            }
        }

        /// <summary>
        /// 绘制字体并返回Icon
        /// </summary>
        /// <param name="size">绘制的尺寸 正方形区域</param>
        /// <param name="str">需要绘制的字体 默认竖直居中</param>
        /// <param name="color">字体的前景色</param>
        /// <param name="font">字体样式</param>
        public Icon StringIcon(string str, Color color, float fontsize = 6, int size = 16, string font = null) {
            Image bufferedimage;
            Font strfont;
            if (_lastsize != fontsize) {
                _lastfont?.Dispose();
                strfont = LoadFont(_lastfont.Name, fontsize);
            }
            else {
                strfont = _lastfont;
            }

            if (_ico == IntPtr.Zero)
                bufferedimage = new Bitmap(size, size, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            else
                bufferedimage = Bitmap.FromHicon(_ico);

            Graphics g = Graphics.FromImage(bufferedimage);
            g.Clear(Color.FromArgb(0, 255, 255, 255));
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            Pen pen = new Pen(color, 1f);
            SizeF infsize = g.MeasureString(str, strfont);
            g.DrawString(str, strfont, pen.Brush,
                new Point(0, (int)((size - infsize.Height) / 2)));
            _ico = (bufferedimage as Bitmap).GetHicon();

            bufferedimage.Dispose();
            g.Dispose();

            return Icon.FromHandle(_ico);
        }

        /// <summary>
        /// 将icon转化为ImageSource
        /// </summary>
        public static ImageSource ToImageSource(Icon icon) {
            Bitmap bitmap = icon.ToBitmap();
            IntPtr hBitmap = bitmap.GetHbitmap();

            ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            DeleteObject(hBitmap);
            return wpfBitmap;
        }

        /// <summary>
        /// 关闭时释放_ico
        /// </summary>
        private void Current_SessionEnding(object sender, SessionEndingCancelEventArgs e) {
            DeleteObject(_ico);
        }
        #endregion

        #region Constructors
        public AreaIconDraw() {
            App.Current.SessionEnding += Current_SessionEnding;
            _privateFontDictionary = new Dictionary<string, PrivateFontCollection>();
            LoadFont(ConstTable.RectNum, 6, ConstTable.MyFont);
        }
        #endregion
    }

}
