using AreaIconCore.Models;
using AreaIconCore.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using YControls.CollectionControls;
using YControls.Command;
using YFrameworkBase;

namespace AreaIconCore.ViewModels {
    /// <summary>
    /// 主页面ViewModel
    /// </summary>
    public class MainWindowViewModel : ViewModelBase<MainWindowViewModel> {

        #region Properties
        private Dictionary<Type, KeyValuePair<string, string>> _iconTable = new Dictionary<Type, KeyValuePair<string, string>> {
            { typeof(MainPage),     new KeyValuePair<string, string>(ConstTable.MainPageIcon,"主页") },
            { typeof(SettingPage),  new KeyValuePair<string, string>(ConstTable.SettingPageIcon,"设置") },
            { typeof(ExtensionPage),new KeyValuePair<string, string>(ConstTable.ExtensionPageIcon,"插件") },
            { typeof(AboutPage),    new KeyValuePair<string, string>(ConstTable.AboutPageIcon,"关于") },

        };
        public Dictionary<Type, KeyValuePair<string, string>> IconTable {
            get => _iconTable;
            set => _iconTable = value;
        }

        #region 
        /// <summary>
        /// 当前页面的图标
        /// </summary>
        private string _contentIconNow;
        public string ContentIconNow {
            get => _contentIconNow;
            set => SetValue(out _contentIconNow, value, ContentIconNow);
        }

        /// <summary>
        /// 当前页面的内容链接
        /// </summary>
        private string _contentPageNow;
        public string ContentPageNow {
            get => _contentPageNow;
            set => SetValue(out _contentPageNow, value, ContentPageNow);
        }
        #endregion

        public CommandBase MainPageCommands { get; set; }
        public CommandBase NavigateCommands { get; set; }
        public CommandBase NavigateTO { get; set; }
        #endregion

        #region Methods
        public void UpdateContentInfo(string icon, string name) {
            ContentIconNow = icon;
            ContentPageNow = name;
        }

        private void InitCommand() {
            MainPageCommands = new CommandBase();
            MainPageCommands.Execution += MainPageCommands_Execution;
            NavigateCommands = new CommandBase();
            NavigateCommands.Execution += NavigateCommands_Execution;
            NavigateTO = new CommandBase();
        }

        private void NavigateCommands_Execution(object para = null) {
            switch (para.ToString()) {
                case "Page_Main":
                    NavigateToLocal(new MainPage());

                    break;
                case "Page_Setting":
                    NavigateToLocal(new SettingPage());

                    break;

                case "Page_Extension":
                    NavigateToLocal(new ExtensionPage());

                    break;
                case "Page_About":
                    NavigateToLocal(new AboutPage());
                    //if (ThemeManager.Instance.ThemeNow["ThemeDic"].ToString() == "Default")
                    //    ThemeManager.Instance.SwitchTheme(App.GetDirectory(DirectoryKind.Theme) + "TestTheme.xaml");
                    //else
                    //    ThemeManager.Instance.SwitchTheme(null);

                    break;
                default: break;
            }
        }

        private void MainPageCommands_Execution(object para = null) {
            switch (para) {
                case "Close":
                    App.Current.MainWindow.Close();
                    break;
                default: break;
            }
        }

        public void NavigateToLocal(Page page) {
            MainWindowViewModel.Singleton.UpdateContentInfo(IconTable[page.GetType()].Key, IconTable[page.GetType()].Value);
            NavigateTO.Execute(new NavigateArgs(page, null));
        }

        public void NavigateTo(Uri page) {
            NavigateTO.Execute(new NavigateArgs(page, null));
        }
        #endregion

        #region Constructors
        public MainWindowViewModel() {
            InitCommand();
        }
        #endregion
    }

}
