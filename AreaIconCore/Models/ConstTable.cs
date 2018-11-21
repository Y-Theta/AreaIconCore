using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaIconCore.Models {
   public class ConstTable {
        #region Properties
        public static string RectNum = "RectNum";
        public static string MyFont = "FontLibrary";


        public static string MainPageIcon {
            get => "\uE80F";
        }
        public static string SettingPageIcon {
            get => "\xE713";
        }
        public static string AboutPageIcon {
            get => "\xE8F2";
        }
        #endregion
    }

}
