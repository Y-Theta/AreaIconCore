using AreaIconCore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    public class AppearanceManager : ViewModelBase<AppearanceManager> {
        #region Properties
        internal const string __ThemeKey = "ThemeDic";
        internal const string __LangKey = "LangDic";

        internal const string __DefaultTheme = "VS_Blue";
        internal const string __DefaultLanguage = "简体中文";

        private bool _inited { get; set; }

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

        /// <summary>
        /// 主题文件列表
        /// </summary>
        public ObservableCollection<String> ThemeDics { get; set; }

        /// <summary>
        /// 选中的主题
        /// </summary>
        private string _selectTheme;
        public string SelectTheme {
            get => _selectTheme;
            set => SetValue(out _selectTheme, value, SelectTheme, OnSelectThemeChanged);
        }
        private bool OnSelectThemeChanged(object op, object np) {
            SwitchTheme(np.ToString());
            CoreSettings.Instance.MainTheme = np.ToString();
            return true;
        }

        /// <summary>
        /// 语言文件列表
        /// </summary>
        public ObservableCollection<String> LanguageDics { get; set; }

        /// <summary>
        /// 选中的语言
        /// </summary>
        private string _selectLanguage;
        public string SelectLanguage {
            get => _selectLanguage;
            set => SetValue(out _selectLanguage, value, SelectLanguage, OnSelectLanguageChanged);
        }
        private bool OnSelectLanguageChanged(object op, object np) {
            SwitchLanguage(np.ToString());
            CoreSettings.Instance.MainLang = np.ToString();
            LanguageChanged?.Invoke(op, np);
            return true;
        }

        /// <summary>
        /// 配置文件的名称与其对应路径
        /// </summary>
        private Dictionary<string, string> DicMap { get; set; }

        public event YPropertyChangedEventHandler LanguageChanged;

        private string _temptheme;

        private string _templang;

        private readonly Collection<ResourceDictionary> AppRes = App.Current.Resources.MergedDictionaries;
        #endregion

        #region Methods

        /// <summary>
        /// 切换主题
        /// </summary>
        /// <param name="themefile">主题文件路径</param>
        public bool SwitchTheme(string themename) {
            var themefile = DicMap[themename];
            if (themefile == _temptheme)
                return true;
            else if (themename == ThemeDics[0]) {
                if (AppRes.Contains(_themedic))
                    AppRes.Remove(_themedic);
                _themedic = new ResourceDictionary { Source = new Uri(ConstTable.DefaultThemeUri, UriKind.Relative) };
                AppRes.Add(_themedic);
                _temptheme = ConstTable.DefaultThemeUri;
                return true;
            }
            else
                return SwitchRes(themefile, ref _temptheme, _themedic);
        }

        /// <summary>
        /// 切换语言文件
        /// </summary>
        /// <param name="langfile">语言文件路径</param>
        public bool SwitchLanguage(string langname) {
            var langfile = DicMap[langname];
            if (langfile == _templang)
                return true;
            else if (langname == LanguageDics[0]) {
                if (AppRes.Contains(_langdic))
                    AppRes.Remove(_langdic);
                _langdic = new ResourceDictionary { Source = new Uri(ConstTable.DefaultLangUri, UriKind.Relative) };
                AppRes.Add(_langdic);
                _templang = ConstTable.DefaultLangUri;
                return true;
            }
            else
                return SwitchRes(langfile, ref _templang, _langdic);
        }

        /// <summary>
        /// 切换资源
        /// </summary>
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

        /// <summary>
        /// 添加窗体监听
        /// </summary>
        private void AddListener() {
            App.Current.MainWindow.IsVisibleChanged += MainWindow_IsVisibleChanged;
        }

        /// <summary>
        /// 在窗体后台时销毁单例
        /// </summary>
        private void MainWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            //后台时销毁单例
            if (!(bool)e.NewValue) {
                Singleton = null;
            }
        }

        /// <summary>
        /// 切换到上次的主题
        /// </summary>
        private void SwitchToLastSelected() {
            SelectTheme = CoreSettings.Instance.MainTheme;
            SelectLanguage = CoreSettings.Instance.MainLang;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init() {
            if (_inited)
                return;
            ThemeDics.Clear();
            LanguageDics.Clear();
            DicMap.Clear();
            //设置主题资源
            _themedic = AppRes.FirstOrDefault(e => { return e.Contains(__ThemeKey); });
            _selectTheme = _themedic[__ThemeKey].ToString();
            _temptheme = _selectTheme;
            //加载默认主题
            ThemeDics.Add(__DefaultTheme);
            DicMap.Add(__DefaultTheme, ConstTable.DefaultThemeUri);
            //加载其它主题
            foreach (var themes in Directory.GetFiles(App.GetDirectory(DirectoryKind.Theme))) {
                ResourceDictionary dic;
                using (FileStream fs = new FileStream(themes, FileMode.Open)) {
                    dic = (ResourceDictionary)XamlReader.Load(fs);
                }
                ThemeDics.Add(dic[__ThemeKey].ToString());
                DicMap.Add(dic[__ThemeKey].ToString(), themes);
                dic = null;
            }
            //设置语言资源
            _langdic = AppRes.FirstOrDefault(e => { return e.Contains(__LangKey); });
            _selectLanguage = _langdic[__LangKey].ToString();
            _templang = _selectLanguage;
            //加载默认语言
            LanguageDics.Add(__DefaultLanguage);
            DicMap.Add(__DefaultLanguage, ConstTable.DefaultLangUri);
            //加载其它文件
            foreach (var langs in Directory.GetFiles(App.GetDirectory(DirectoryKind.Lang))) {
                ResourceDictionary dic;
                using (FileStream fs = new FileStream(langs, FileMode.Open)) {
                    dic = (ResourceDictionary)XamlReader.Load(fs);
                }
                LanguageDics.Add(dic[__LangKey].ToString());
                DicMap.Add(dic[__LangKey].ToString(), langs);
                dic = null;
            }
            //
            AddListener();
            SwitchToLastSelected();
            _inited = true;
        }
        #endregion

        #region Constructors
        public AppearanceManager() {
            ThemeDics = new ObservableCollection<string>();
            LanguageDics = new ObservableCollection<string>();
            DicMap = new Dictionary<string, string>();
            Init();
        }
        #endregion
    }

}
