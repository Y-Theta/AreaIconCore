using ColorD = System.Drawing.Color;
using System.Windows;
using AreaIconCore.Services;
using YControls.AreaIconWindow;
using YControls.FlowControls;
using YFrameworkBase.Setting;
using AreaIconCore.ViewModels;
using YControls.Command;
using System;
using static YControls.WinAPI.DllImportMethods;
using System.Runtime.InteropServices;
using System.Windows.Media;
using AreaIconCore.Models;
using YControls.InterAction;
using ToastHelper;
using AreaIconCore.Views.Pages;

namespace AreaIconCore.Views {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : YT_Window {

        #region Properties
        /// <summary>
        /// 失去焦点时边框
        /// </summary>
        public Brush FocusLostBrush {
            get { return (Brush)GetValue(FocusLostBrushProperty); }
            set { SetValue(FocusLostBrushProperty, value); }
        }
        public static readonly DependencyProperty FocusLostBrushProperty =
            DependencyProperty.Register("FocusLostBrush", typeof(Brush),
                typeof(MainWindow), new PropertyMetadata(null));

        /// <summary>
        /// 获得焦点时边框
        /// </summary>
        public Brush FocusGotColor {
            get { return (Brush)GetValue(FocusGotColorProperty); }
            set { SetValue(FocusGotColorProperty, value); }
        }
        public static readonly DependencyProperty FocusGotColorProperty =
            DependencyProperty.Register("FocusGotColor", typeof(Brush),
                typeof(MainWindow), new PropertyMetadata(null));
        #endregion

        #region Methods
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
        }

        #endregion

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {

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
                    AreaIcons[ext.Name].DContextmenu = App.Current.Resources["AreaIconContextMenu"] as YT_ContextMenu;
                    var popup = new YT_PopupBase();
                    popup.Style = App.Current.Resources["AeroPopup"] as Style;
                    AreaIcons[ext.Name].CheckPop = popup;
                }
                if (ext.Application.ContainsKey(ExtensionContract.ApplicationScenario.MainPage)) {

                }
            }
            (DataContext as MainWindowViewModel).NavigateToLocal(new MainPage());
        }

        private void MainWindow_IconMouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (Visibility == Visibility.Visible)
                App.Current.MainWindow.Hide();
            else
                App.Current.MainWindow.Show();
        }

        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
    }
}
