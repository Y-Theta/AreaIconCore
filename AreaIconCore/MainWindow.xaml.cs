using System;
using System.Drawing;
using System.Windows;
using AreaIconCore.Services;
using YControls.AreaIconWindow;

namespace AreaIconCore {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : YT_Window {
        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            DataContext = this;
            #region
            //AllowAreaIcon = true;
            //RegisterAreaIcon(_iconName);
            // AreaIcons[_iconName].IconMouseDoubleClick += MainWindow_IconMouseDoubleClick;
            //_info = new SettingNodeInfo { Name = "Y_T", Key = "111", Path = @"ooo" };
            //SettingNodeInfo info1 = new SettingNodeInfo { Name = "Y_T", Key = "222", Path = @"ooo" };
            //SettingNodeInfo info2 = new SettingNodeInfo { Name = "Y_T", Key = "333", Path = @"ooo" };
            //SettingNodeInfo info3 = new SettingNodeInfo { Name = "Y_T", Key = "444", Path = @"ooo" };
            //ObservableCollection<SettingNodeInfo> infolist = new ObservableCollection<SettingNodeInfo>() {
            //    _info,
            //    info2,
            //    info3,
            //    info1,
            //};
            //Info = infolist;
            //CoreSettings.Instence.Info = new SettingNodeInfo { Name = "corsetting.xml", Path = @"Settings\" };
            //SettingManager.Instence.AddSetting("Core", CoreSettings.Instence);
            //CoreSettings.Instence.Extensions.Add(new ExtensionInfo("1111", true));
            //CoreSettings.Instence.Extensions.Add(new ExtensionInfo("2222", true));
            //CoreSettings.Instence.Extensions.Add(new ExtensionInfo("3333", true));
            //CoreSettings.Instence.Extensions.Add(new ExtensionInfo("4444", true));
            //CoreSettings.Instence.MainColor = ColorD.FromArgb(80, 80, 80, 80);
            //SettingManager.Instence.Config = new SettingNodeInfo { Name = "setting.xml" };
            #endregion
            //foreach (var ext in HostAdapter.Instance.ExtensionDirectory.Values) {
            //    if (ext.Application.ContainsKey(ExtensionContract.ApplicationScenario.AreaIcon)) {
            //        if (!AllowAreaIcon)
            //            AllowAreaIcon = true;
            //        RegisterAreaIcon(ext.Name);
            //        AreaIcons[ext.Name].Areaicon = (Icon)ext.Run(ExtensionContract.ApplicationScenario.AreaIcon);
            //        AreaIcons[ext.Name].IconMouseDoubleClick += MainWindow_IconMouseDoubleClick;
            //    }
            //    if (ext.Application.ContainsKey(ExtensionContract.ApplicationScenario.MainPage)) {
            //        MainFrame.Content = ext.Run(ExtensionContract.ApplicationScenario.MainPage);
            //    }
            //}
        }

        private void MainWindow_IconMouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (Visibility == Visibility.Visible)
                App.Current.MainWindow.Hide();
            else
                App.Current.MainWindow.Show();
        }

        private void FIconToggleButton_Checked(object sender, RoutedEventArgs e) {

        }

        private void FIconToggleButton_Checked_1(object sender, RoutedEventArgs e) {

        }
    }
}
