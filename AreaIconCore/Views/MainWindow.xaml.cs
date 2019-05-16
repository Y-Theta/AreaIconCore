using Icon = System.Drawing.Icon;
using System.Windows;
using AreaIconCore.Services;
using YControls.AreaIconWindow;
using AreaIconCore.ViewModels;
using AreaIconCore.Models;
using AreaIconCore.Views.Pages;
using ExtensionContract;
using ToastHelper;

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
            NotificationService ser = new NotificationService();
            ser.Notify("2323", "2323", true);
        }

        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
    }
}
