using AreaIconCore.Models;
using AreaIconCore.Services;
using AreaIconCore.Views;
using System;
using System.Windows;
using ToastHelper;

namespace AreaIconCore {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        InnerProcessAssemblyManager _manger;

        public App() {
            _manger = new InnerProcessAssemblyManager();
            _manger.Directorys.Add(GetDirectory(DirectoryKind.Bin));
            _manger.HockResolve(AppDomain.CurrentDomain);
            Startup += App_Startup;
        }

        /// <summary>
        /// 获得应用下的相对路径
        /// </summary>
        public static String GetDirectory(DirectoryKind kind = DirectoryKind.Root) {
            switch (kind) {
                case DirectoryKind.Root:
                    return AppDomain.CurrentDomain.BaseDirectory;
                case DirectoryKind.Bin:
                    return AppDomain.CurrentDomain.BaseDirectory + @"bin\";
                case DirectoryKind.Extension:
                    return AppDomain.CurrentDomain.BaseDirectory + @"extensions\";
                case DirectoryKind.Setting:
                    return AppDomain.CurrentDomain.BaseDirectory + @"setting\";
                case DirectoryKind.Config:
                    return AppDomain.CurrentDomain.BaseDirectory + @"data\";
                default:
                    return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        private void App_Startup(object sender, StartupEventArgs e) {
            DesktopNotificationManagerCompat.RegisterAumidAndComServer<AreaIconToast>("Y_T" + nameof(AreaIconCore));
            DesktopNotificationManagerCompat.RegisterActivator<AreaIconToast>();

            HostAdapter.Instance.PluginsPath = App.GetDirectory(DirectoryKind.Extension);
            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
