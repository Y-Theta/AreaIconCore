using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using YFrameworkBase;

namespace AreaIconCore.Services {
    /// <summary>
    /// 托盘图标绘制函数
    /// </summary>
    internal class AreaIconDraw : SingletonBase<AreaIconDraw> {
        #region Properties
        private const string _numfontfamily = "FontLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

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
                    try {
                        var asb = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => {
                            return a.GetName().Name.Equals(assemblyname);
                        });
                        if (asb is null)
                            asb = AppDomain.CurrentDomain.Load(_numfontfamily);
                        using (var strs = asb.GetManifestResourceStream(string.Format(@"{0}.{1}.ttf", assemblyname, familyname))) {
                            myText = new byte[strs.Length];
                            strs.Read(myText, 0, myText.Length);
                        }
                    }
                    catch {
                        //TODO:字体不存在
                    }
                }
                else {
                    //在资源文件中搜索
                    var asb = Assembly.GetExecutingAssembly();
                    try {
                        using (var strs = asb.GetManifestResourceStream(string.Format(@"{0}.{1}.ttf", asb.GetName().Name, familyname))) {
                            myText = new byte[strs.Length];
                            strs.Read(myText, 0, myText.Length);
                        }
                    }
                    catch {
                        //TODO:字体不存在

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
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            Pen pen = new Pen(color, 1f);
            SizeF infsize = g.MeasureString(str, strfont);
            g.DrawString(str, strfont, pen.Brush,
                new Point(0, (int)((size - infsize.Height) / 2)));
            _ico = (bufferedimage as Bitmap).GetHicon();

            bufferedimage.Dispose();
            g.Dispose();

            return Icon.FromHandle(_ico);
        }
        #endregion

        #region Constructors
        public AreaIconDraw() {
            _privateFontDictionary = new Dictionary<string, PrivateFontCollection>();
        }
        #endregion
    }

}
