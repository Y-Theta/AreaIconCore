using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using YFrameworkBase;

namespace AreaIconCore.Services {
    public class LanguageManager : ViewModelBase<LanguageManager> {
        #region Properties
        private XmlDataProvider _languageres;
        public XmlDataProvider LanguageRes {
            get => _languageres;
            private set => SetValue(out _languageres, value, LanguageRes);
        }
        #endregion

        #region Methods
        public void UpdataSource() {
            System.Windows.Forms.Application.Restart();
            App.Current.Shutdown();
        }

        public void Save() {
            LanguageRes.Document.Save(App.GetDirectory(Models.DirectoryKind.Lang) + "zh_cn.xml");
            
        }
        #endregion

        #region Constructors
        public LanguageManager() {
            LanguageRes = new XmlDataProvider();
            LanguageRes.Source = new Uri(App.GetDirectory(Models.DirectoryKind.Lang) + "zh_cn.xml");
            LanguageRes.XPath = "Config";
        }
        #endregion
    }

}
