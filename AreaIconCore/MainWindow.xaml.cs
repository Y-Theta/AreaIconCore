using System;
using System.Drawing;
using ColorD = System.Drawing.Color;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AreaIconCore.Services;
using YControls.AreaIconWindow;
using YControls.Dialogs;
using YControls.FlowControls;
using System.Linq;

namespace AreaIconCore {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : YT_Window {
        YT_PopupBase Pop = new YT_PopupBase();

        public string TestText {
            get { return (string)GetValue(TestTextProperty); }
            set { SetValue(TestTextProperty, value); }
        }
        public static readonly DependencyProperty TestTextProperty =
            DependencyProperty.Register("TestText", typeof(string),
                typeof(MainWindow), new PropertyMetadata(""));


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
            foreach (var ext in HostAdapter.Instance.ExtensionDirectory.Values) {
                if (ext.Application.ContainsKey(ExtensionContract.ApplicationScenario.AreaIcon)) {
                    if (!AllowAreaIcon)
                        AllowAreaIcon = true;
                    RegisterAreaIcon(ext.Name);
                    AreaIcons[ext.Name].IconMouseDoubleClick += MainWindow_IconMouseDoubleClick;
                }
                if (ext.Application.ContainsKey(ExtensionContract.ApplicationScenario.MainPage)) {

                }
            }
        }

        private void MainWindow_IconMouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (Visibility == Visibility.Visible)
                App.Current.MainWindow.Hide();
            else
                App.Current.MainWindow.Show();
        }

        private void FIconToggleButton_Checked(object sender, RoutedEventArgs e) {
            var ContextM = FindResource("ContextM") as YT_ContextMenu;
            ContextM.Placement = System.Windows.Controls.Primitives.PlacementMode.Mouse;
            ContextM.IsOpen = true;
        }

        private void FIconToggleButton_Checked_1(object sender, RoutedEventArgs e) {
            //AreaIconDraw.Instance.LoadFont("RectNum", 6, "FontLibrary");
            //AreaIcons["D6_ex"].Areaicon = AreaIconDraw.Instance.StringIcon(16, "88", ColorD.AliceBlue, 6, "RectNum");
        }

        private void FIconButton_Click(object sender, RoutedEventArgs e) {

        }
    }
}
