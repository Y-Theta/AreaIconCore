using ColorD = System.Drawing.Color;
using Icon = System.Drawing.Icon;
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
            if (!AllowAreaIcon)
                AllowAreaIcon = true;
            AreaIconDraw.Instance.LoadFont(ConstTable.RectNum, 6, ConstTable.MyFont);

            foreach (var ext in HostAdapter.Instance.ExtensionDirectory.Values) {
                if (ext.Application.ContainsKey(ExtensionContract.ApplicationScenario.AreaIcon)) {
                    RegisterAreaIcon(ext.Name);
                    AreaIcons[ext.Name].IconMouseDoubleClick += MainWindow_IconMouseDoubleClick;
                    AreaIcons[ext.Name].Areaicon = AreaIconDraw.Instance.StringIcon("33", ColorD.White);
                    AreaIcons[ext.Name].DContextmenu = App.CreateAreaIconMenu();
                    AreaIcons[ext.Name].CheckPop = App.CreatAreaPopup(App.Current.Resources["TextKey"] as Grid);
                }
                if (ext.Application.ContainsKey(ExtensionContract.ApplicationScenario.MainPage)) {

                }
            }
            if (AreaIcons.Count == 0) {
                RegisterAreaIcon(nameof(AreaIconCore));
                AreaIcons[nameof(AreaIconCore)].IconMouseDoubleClick += MainWindow_IconMouseDoubleClick;
                AreaIcons[nameof(AreaIconCore)].Areaicon = new Icon(ConstTable.AppIcon);
                AreaIcons[nameof(AreaIconCore)].AreaText = "AreaIconCore";
                AreaIcons[nameof(AreaIconCore)].DContextmenu = App.CreateAreaIconMenu();
            }
            (DataContext as MainWindowViewModel).NavigateToLocal(new MainPage());
        }

        private void MainWindow_IconMouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e) {
            App.ToastHelper.Notify(ConstTable.AppIcon, nameof(AreaIconCore), "sss");
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
