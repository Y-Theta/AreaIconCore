using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WNMP_ex.Services;

namespace WNMP_ex.Controls {
    /// <summary>
    /// ServerPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ServerPanel : UserControl {
        public ServerPanel() {
            InitializeComponent();
            Loaded += ServerPanel_Loaded;
            ServerManager.Instance.OnButtonClick += Instance_OnServerStatusChange;
        }

        private void Instance_OnServerStatusChange(object para) {
            switch (para.ToString()) {
                case "ON":
                    Server_Nginx.IsServerRuning = Server_PHP.IsServerRuning = true;
                    Server_MySQL.IsServerRuning = Server_MySQL.IsServerRuning | WNMP_Setting.Instance.Defaultstatus.Equals(ServerStatus.NMP);
                    break;
                case "OFF":
                    Server_Nginx.IsServerRuning = Server_PHP.IsServerRuning = true;
                    Server_MySQL.IsServerRuning = WNMP_Setting.Instance.Defaultstatus.Equals(ServerStatus.NMP) ? false : Server_MySQL.IsServerRuning;
                    break;
            }
        }

        private void ServerPanel_Loaded(object sender, RoutedEventArgs e) {
            Server_PHP.SelfTest();
            Server_Nginx.SelfTest();
            Server_MySQL.SelfTest();
        }
    }
}
