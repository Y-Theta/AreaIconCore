using AreaIconCore.Services;
using HttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YFrameworkBase.DataAccessor;

namespace AreaIconCore.Views.Pages {
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page {

        public MainPage() {
            InitializeComponent();
            ToastServices.ToastCallback += ToastServices_ToastCallback;
        }

        private void FIconButton_Click(object sender, RoutedEventArgs e) {
            App.ToastNotifier.Notify();
        }

        private void ToastServices_ToastCallback(string app, string arg, List<KeyValuePair<string, string>> kvs) {
            
        }
    }
}
