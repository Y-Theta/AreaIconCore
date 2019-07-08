using AreaIconCore.Models;
using AreaIconCore.Services;
using AreaIconCore.Views;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using YControls.FlowControls;
using YFrameworkBase;
using YFrameworkBase.RegOperator;
using Icon = System.Drawing.Icon;

namespace AreaIconCore {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        private InnerProcessAssemblyManager _manger;
        private static readonly string APP_ID = "Areaicon Core";
        private static readonly string _basedir = Process.GetCurrentProcess().MainModule.FileName.Substring(0, Process.GetCurrentProcess().MainModule.FileName.LastIndexOf('\\') + 1);

        /// <summary>
        /// 获得应用下的相对路径
        /// </summary>
        public static String GetDirectory(DirectoryKind kind = DirectoryKind.Root) {
            switch (kind) {
                case DirectoryKind.Root:
                    return _basedir;
                case DirectoryKind.Bin:
                    return _basedir + @"bin\";
                case DirectoryKind.Extension:
                    return _basedir + @"extensions\";
                case DirectoryKind.Setting:
                    return _basedir + @"settings\";
                case DirectoryKind.Config:
                    return _basedir + @"data\";
                case DirectoryKind.Theme:
                    return _basedir + @"data\theme\";
                case DirectoryKind.Cache:
                    return _basedir + @"data\cache\";
                case DirectoryKind.Lang:
                    return _basedir + @"data\lang\";
                default:
                    return null;
            }
        }

        /// <summary>
        /// 在首次运行时创建需要文件夹
        /// 完善路径文件夹
        /// </summary>
        public static void CompleteDirectory() {
            Directory.CreateDirectory(GetDirectory());
            Directory.CreateDirectory(GetDirectory(DirectoryKind.Bin));
            Directory.CreateDirectory(GetDirectory(DirectoryKind.Cache));
            Directory.CreateDirectory(GetDirectory(DirectoryKind.Config));
            Directory.CreateDirectory(GetDirectory(DirectoryKind.Extension));
            Directory.CreateDirectory(GetDirectory(DirectoryKind.Lang));
            Directory.CreateDirectory(GetDirectory(DirectoryKind.Setting));
            Directory.CreateDirectory(GetDirectory(DirectoryKind.Theme));
        }

        /// <summary>
        /// 统一调用此方法生成托盘菜单的Popup
        /// 可在应用程序内部获得统一风格
        /// </summary>
        public static YT_PopupBase CreatAreaPopup(UIElement content) {
            YT_PopupBase popup = new YT_PopupBase();
            popup.Child = content;
            popup.Style = Current.FindResource("AreaPopup") as Style;
            return popup;
        }

        /// <summary>
        /// 从资源构造一个托盘图标的右键菜单
        /// </summary>
        public static YT_ContextMenu CreateAreaIconMenu() {
            YT_ContextMenu menu = new YT_ContextMenu { Style = Current.Resources["WinXTaskBarContextMenuStyle"] as Style };
            var item1 = new YT_MenuItem { Style = Current.Resources["AreaContextMenu_ShowMainWindow"] as Style };
            menu.Items.Add(item1);
            var item2 = new YT_MenuItem { Style = Current.Resources["AreaContextMenu_Setting"] as Style };
            menu.Items.Add(item2);
            var sep1 = new Separator { Style = Current.Resources["AreaContextMenuSeperator"] as Style };
            menu.Items.Add(sep1);
            var item3 = new YT_MenuItem { Style = Current.Resources["AreaContextMenu_Exit"] as Style };
            menu.Items.Add(item3);
            menu.Placement = System.Windows.Controls.Primitives.PlacementMode.Top;
            return menu;
        }

        /// <summary>
        /// 
        /// </summary>
        private bool Singleton_LanguageChanged(object op, object np) {
            return true;
        }

        /// <summary>
        /// 初始化窗体操作
        /// </summary>
        protected void InitWindow(params string[] args) {
            MainWindow window = new MainWindow();
            window.Show();
            if (args.Length > 0)
                window.Hide();
            window.IsVisibleChanged += Window_IsVisibleChanged;
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            //在窗体不可见时将内存释放一些
            if (!(bool)e.NewValue) {
                WinAPI.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle.ToInt32(), -1, -1);
            }
        }

        /// <summary>
        /// 主窗口初始化前操作
        /// </summary>
        protected void BeforeWindowInit() {
            //启用Win10消息通知

            //加载插件
            HostAdapter.Instance.PluginsPath = App.GetDirectory(DirectoryKind.Extension);
        }

        private void ToastHelper_ToastCallback(string app, string arg, System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>> kvs) {
            Console.WriteLine(app + "   " + arg);
            kvs.ForEach(kv => Console.WriteLine(kv.Key + "   " + kv.Value));
        }

        /// <summary>
        /// 主窗口初始化后操作
        /// </summary>
        protected void AfterWindowInit() {
            CoreSettings.Instance.SettingChanged += Instance_SettingChanged;
            //初始化样式资源
            AppearanceManager.Singleton.Init();
            //监听语言设置变更
            AppearanceManager.Singleton.LanguageChanged += Singleton_LanguageChanged;
        }

        private bool Instance_SettingChanged(object sender, SettingChangeArgs e) {
            if (e.Name == "AutoRun") {
                RegAccessor.SetAutorun(nameof(AreaIconCore), $"\"{Process.GetCurrentProcess().MainModule.FileName}\"" + "-silent", (bool)e.NewValue);
                if ((bool)e.NewValue)
                    PopupMessageManager.Instance.Message("已设置为开机自动启动");
                else
                    PopupMessageManager.Instance.Message("已关闭开机自动启动");
            }
            return true;
        }

        /// <summary>
        /// 主程序启动操作
        /// </summary>
        private void App_Startup(object sender, StartupEventArgs e) {
            BeforeWindowInit();
            //初始化主窗口
            InitWindow(e.Args);
            AfterWindowInit();
        }

        /// <summary>
        /// 主程序退出操作
        /// </summary>
        private void App_SessionEnding(object sender, SessionEndingCancelEventArgs e) {

        }

        /// <summary>
        /// 在查找资源出错时通过UI反馈
        /// </summary>
        public static T FindRes<T>(string reskey) {
            try {
                return (T)App.Current.FindResource(reskey);
            }
            catch {
                return default(T);
            }
        }

        public App() {
            CompleteDirectory();
            _manger = new InnerProcessAssemblyManager();
            _manger.Directorys.Add(GetDirectory(DirectoryKind.Bin));
            _manger.HockResolve(AppDomain.CurrentDomain);
            Startup += App_Startup;
            SessionEnding += App_SessionEnding;
        }
    }
}
