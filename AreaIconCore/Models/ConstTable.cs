using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaIconCore.Models {
   public class ConstTable {
        #region Properties
        public static readonly string RectNum = "RectNum";
        public static readonly string MyFont = "FontLibrary";

        public static readonly string AppIcon = App.GetDirectory(DirectoryKind.Config) + "Icon_128.ico";

        public static readonly string DefaultThemeUri = @"/Views/Appearance/default_theme.xaml";
        public static readonly string DefaultLangUri = @"/Views/Appearance/default_language.xaml";

        #endregion
    }

}
