using AreaIconCore.Models;
using AreaIconCore.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using YFrameworkBase.Setting;

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
            HostAdapter.Instance.PluginsPath = App.GetDirectory(DirectoryKind.Extension);
            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
