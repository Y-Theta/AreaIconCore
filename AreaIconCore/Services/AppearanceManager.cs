using AreaIconCore.Models;
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
    public class AppearanceManager : SingletonBase<AppearanceManager> {
        #region Properties
        private ResourceDictionary _themedic { get; set; }
        /// <summary>
        /// 当前主题资源字典
        /// </summary>
        public ResourceDictionary ThemeNow {
            get => _themedic;
        }

        private ResourceDictionary _langdic { get; set; }
        /// <summary>
        /// 当前语言资源字典
        /// </summary>
        public ResourceDictionary LangNow {
            get => _langdic;
        }

        private string _temptheme;

        private string _templang;

        private readonly Collection<ResourceDictionary> AppRes = App.Current.Resources.MergedDictionaries;
        #endregion

        #region Methods

        /// <summary>
        /// 切换主题
        /// </summary>
        /// <param name="themefile">主题文件路径</param>
        public bool SwitchTheme(string themefile) {
            if (themefile == _temptheme)
                return true;
            if (string.IsNullOrEmpty(themefile)) {
                _themedic = new ResourceDictionary { Source = new Uri(ConstTable.DefaultThemeUri, UriKind.Relative) };
                AppRes.Add(_themedic);
                _temptheme = null;
                return true;
            }
            return SwitchRes(themefile, ref _temptheme, _themedic);
        }

        /// <summary>
        /// 切换语言文件
        /// </summary>
        /// <param name="langfile">语言文件路径</param>
        public bool SwitchLanguage(string langfile) {
            if (langfile == _templang)
                return true;
            if (string.IsNullOrEmpty(langfile)) {
                _langdic = new ResourceDictionary { Source = new Uri(ConstTable.DefaultLangUri, UriKind.Relative) };
                AppRes.Add(_langdic);
                _templang = null;
                return true;
            }
            return SwitchRes(langfile, ref _templang, _langdic);
        }

        private bool SwitchRes(string filename, ref string tempfile, ResourceDictionary resdic) {
            AppRes.Remove(resdic);
            resdic = null;
            ResourceDictionary dic;
            using (FileStream fs = new FileStream(filename, FileMode.Open)) {
                dic = (ResourceDictionary)XamlReader.Load(fs);
            }
            if (dic == null)
                return false;
            resdic = dic;
            AppRes.Add(resdic);
            tempfile = filename;
            return true;
        }
        #endregion

        #region Constructors
        public AppearanceManager() {
            _themedic = AppRes.FirstOrDefault(e => { return e.Contains("ThemeDic"); }) as ResourceDictionary;
            _langdic = AppRes.FirstOrDefault(e => { return e.Contains("LangDic"); }) as ResourceDictionary;
        }
        #endregion
    }

}
