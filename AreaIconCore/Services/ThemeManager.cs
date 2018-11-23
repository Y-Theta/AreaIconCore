using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using YFrameworkBase;

namespace AreaIconCore.Services {
    /// <summary>
    /// 主题管理
    /// </summary>
    public class ThemeManager : SingletonBase<ThemeManager> {
        #region Properties
        private ResourceDictionary _themedic { get; set; }
        /// <summary>
        /// 当前主题资源字典
        /// </summary>
        public ResourceDictionary ThemeNow {
            get => _themedic;
        }

        private string _tempdic = null;

        private readonly Collection<ResourceDictionary> AppRes = App.Current.Resources.MergedDictionaries;
        #endregion

        #region Methods

        /// <summary>
        /// 切换主题
        /// </summary>
        /// <param name="themefile">主题文件路径</param>
        public bool SwitchTheme(string themefile) {
            if (themefile == _tempdic)
                return true;
            if (string.IsNullOrEmpty(themefile)) {
                _themedic = new ResourceDictionary { Source = new Uri(@"/Views/DefaultTheme/ThemeNow.xaml", UriKind.Relative) };
                AppRes.Add(_themedic);
                _tempdic = null;
                return true;
            }
            AppRes.Remove(_themedic);
            _themedic = null;
            ResourceDictionary dic;
            using (FileStream fs = new FileStream(themefile, FileMode.Open)) {
                dic = (ResourceDictionary)XamlReader.Load(fs);
            }
            if (dic == null)
                return false;
            _themedic = dic;
            AppRes.Add(_themedic);
            _tempdic = themefile;
            return true;
        }
        #endregion

        #region Constructors
        public ThemeManager() {
            _themedic = AppRes.FirstOrDefault(e => { return e.Contains("ThemeDic"); }) as ResourceDictionary;
        }
        #endregion
    }

}
