using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows;
using ColorD = System.Drawing.Color;
using Image = System.Drawing.Image;
using Pen = System.Drawing.Pen;
using Color = System.Windows.Media.Color;
using Drawing = System.Drawing;
using AreaIconCore.Services;
using YFrameworkBase.Setting;
using YFrameworkBase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Controls;
using AreaIconCore.Models;
using YControls.AreaIconWindow;

namespace AreaIconCore {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : YT_Window {
        private IntPtr _ico = IntPtr.Zero;

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
            HostAdapter.Instance.PluginsPath = AppDomain.CurrentDomain.BaseDirectory + @"extensions\";
        }

        private void MainWindow_IconMouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e) {
            GC.Collect(2, GCCollectionMode.Forced, true);
            if (Visibility == Visibility.Hidden)
                Show();
            else
                Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
           
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            
        }
    }
}
