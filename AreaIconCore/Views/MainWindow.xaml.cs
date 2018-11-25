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
using YControls.Dialogs;
using System.Windows.Controls;

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
            AreaIconDraw.Instance.LoadFont(ConstTable.RectNum, 6, ConstTable.MyFont);
            AppearanceManager.Singleton.LanguageChanged += Singleton_LanguageChanged;
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
                    AreaIcons[ext.Name].Areaicon = AreaIconDraw.Instance.StringIcon("33", ColorD.White);
                    AreaIcons[ext.Name].DContextmenu = CreateAreaIconMenu();
                    // var popup = new YT_PopupBase();
                    //popup.Style = App.Current.Resources["AeroPopup"] as Style;
                    //AreaIcons[ext.Name].CheckPop = popup;
                }
                if (ext.Application.ContainsKey(ExtensionContract.ApplicationScenario.MainPage)) {

                }
            }
            (DataContext as MainWindowViewModel).NavigateToLocal(new MainPage());
        }

        private bool Singleton_LanguageChanged(object op, object np) {
            AreaIcons["IPGW_ex"].DContextmenu = null;
            AreaIcons["IPGW_ex"].DContextmenu = CreateAreaIconMenu();
            return true;
        }

        private void MainWindow_IconMouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e) {
            App.ToastHelper.Notify("", "hello", "new toast");
        }

        /// <summary>
        /// 从资源构造一个托盘图标的右键菜单
        /// </summary>
        private YT_ContextMenu CreateAreaIconMenu() {
            YT_ContextMenu menu = new YT_ContextMenu();
            menu.Style = App.Current.Resources["WinXTaskBarContextMenuStyle"] as Style;
            var item1 = new YT_MenuItem();
            item1.Style = App.Current.Resources["AreaContextMenu_ShowMainWindow"] as Style;
            menu.Items.Add(item1);
            var item2 = new YT_MenuItem();
            item2.Style = App.Current.Resources["AreaContextMenu_Setting"] as Style;
            menu.Items.Add(item2);
            var sep1 = new Separator();
            sep1.Style = App.Current.Resources["AreaContextMenuSeperator"] as Style;
            menu.Items.Add(sep1);
            var item3 = new YT_MenuItem();
            item3.Style = App.Current.Resources["AreaContextMenu_Exit"] as Style;
            menu.Items.Add(item3);
            return menu;
        }

        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var dialog = new YT_DialogBase();
            dialog.Style = App.Current.Resources["EnsureDialog"] as Style;
            dialog.ShowDialog(App.Current.MainWindow);
        }
    }
}
