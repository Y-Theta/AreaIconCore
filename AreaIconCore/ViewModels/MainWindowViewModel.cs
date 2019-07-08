using AreaIconCore.Views.Pages;
using System;
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

        /// <summary>
        /// 是否为插件中的页面
        /// </summary>
        private bool _extensionpage;
        public bool ExtensionPage {
            get => _extensionpage;
            set => SetValue(out _extensionpage, value, ExtensionPage);
        }
        #endregion

        public CommandBase MainPageCommands { get; set; }

        public CommandBase NavigateCommands { get; set; }

        public CommandBase AreaContextMenuCommands { get; set; }

        public CommandBase FrameCommand { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// 托盘右键菜单的命令
        /// </summary>
        private void AreaContextMenuCommands_Execution(object para = null) {
            switch (para.ToString()) {
                case "Show":
                    if (App.Current.MainWindow.Visibility == System.Windows.Visibility.Visible)
                        return;
                    else
                        App.Current.MainWindow.Show();
                    break;
                case "Setting":
                    NavigateToLocal(new SettingPage());
                    break;
                case "Exit":
                    App.Current.Shutdown();
                    break;
                default: break;
            }
        }

        /// <summary>
        /// 导航菜单的命令
        /// </summary>
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
                    break;
                default: break;
            }
        }

        /// <summary>
        /// 其它标题栏菜单的命令
        /// </summary>
        private void MainPageCommands_Execution(object para = null) {
            switch (para) {
                case "Back":
                    NavigateToLocal(new ExtensionPage());
                    break;
                case "Close":
                    App.Current.MainWindow.Hide();
                    break;
                default: break;
            }
        }

        /// <summary>
        /// 导航到本地页面
        /// </summary>
        public void NavigateToLocal(Page page) {
            ExtensionPage = false;
            if (page.GetType() == typeof(MainPage))
                UpdateContentInfo(App.Current.Resources["Icon_MainPage"] as String, App.Current.Resources["Label_MainPage"] as String);
            else if (page.GetType() == typeof(SettingPage))
                UpdateContentInfo(App.Current.Resources["Icon_SettingPage"] as String, App.Current.Resources["Label_SettingPage"] as String);
            else if (page.GetType() == typeof(ExtensionPage))
                UpdateContentInfo(App.Current.Resources["Icon_ExtensionPage"] as String, App.Current.Resources["Label_ExtensionPage"] as String);
            else if (page.GetType() == typeof(AboutPage))
                UpdateContentInfo(App.Current.Resources["Icon_AboutPage"] as String, App.Current.Resources["Label_AboutPage"] as String);
            FrameCommand.Execute(new NavigateArgs(page, null));
        }

        public void NavigateTo(Uri page) {
            ExtensionPage = true;
            FrameCommand.Execute(new NavigateArgs(page, null));
        }

        public void NavigateTo(object content) {
            ExtensionPage = true;
            FrameCommand.Execute(new NavigateArgs(content, null));
        }

        private void UpdateContentInfo(string icon, string name) {
            ContentIconNow = icon;
            ContentPageNow = name;
        }

        private void InitCommand() {
            MainPageCommands = new CommandBase();
            MainPageCommands.Execution += MainPageCommands_Execution;
            NavigateCommands = new CommandBase();
            NavigateCommands.Execution += NavigateCommands_Execution;
            FrameCommand = new CommandBase();
            AreaContextMenuCommands = new CommandBase();
            AreaContextMenuCommands.Execution += AreaContextMenuCommands_Execution;
        }
        #endregion

        #region Constructors
        public MainWindowViewModel() {
            InitCommand();
        }
        #endregion
    }

}
