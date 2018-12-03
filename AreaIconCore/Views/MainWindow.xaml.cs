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
using ExtensionContract;
using YFrameworkBase.DataAccessor;
using HttpServices;
using HttpServices.DataProviders;
using System.Collections.Generic;

namespace AreaIconCore.Views {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : YT_Window {

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            //允许托盘图标
            if (!AllowAreaIcon)
                AllowAreaIcon = true;

            foreach (var ext in HostAdapter.Instance.ExtensionDirectory.Values) {
                if (ext.Application.ContainsKey(ApplicationScenario.AreaIcon)) {
                    RegisterAreaIcon(ext.Name);
                    AreaIcons[ext.Name].IconMouseDoubleClick += MainWindow_IconMouseDoubleClick;
                    AreaIcons[ext.Name].Areaicon = (Icon)ext.Run(ApplicationScenario.AreaIcon);
                    AreaIcons[ext.Name].AreaText = ext.Name;
                    AreaIcons[ext.Name].DContextmenu = App.CreateAreaIconMenu();
                    AreaIcons[ext.Name].CheckPop = App.CreatAreaPopup((UIElement)ext.Run(ApplicationScenario.AreaPopup));
                }
                if (ext.Application.ContainsKey(ApplicationScenario.MainPage)) {
                }
            }
            if (AreaIcons.Count == 0) {
                RegisterAreaIcon(nameof(AreaIconCore));
                AreaIcons[nameof(AreaIconCore)].IconMouseDoubleClick += MainWindow_IconMouseDoubleClick;
                AreaIcons[nameof(AreaIconCore)].Areaicon = new Icon(ConstTable.AppIcon);
                AreaIcons[nameof(AreaIconCore)].AreaText = nameof(AreaIconCore);
                AreaIcons[nameof(AreaIconCore)].DContextmenu = App.CreateAreaIconMenu();
            }
            //导航到主页
           ((MainWindowViewModel)DataContext).NavigateToLocal(new MainPage());

            //XmlDataProvider.Instance.AddNode<WebInfoContainer>(new WebInfoContainer {
            //    Name = "NEU_Ipgw"

            //});
        }

        private void MainWindow_IconMouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e) {
            App.ToastHelper.Notify(ConstTable.AppIcon, nameof(AreaIconCore), "HELLO", new ToastCommands[] {
                 new ToastCommands("Invoke_YES","YES"),
                 new ToastCommands("Invoke_NO","NO"),
             });
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
