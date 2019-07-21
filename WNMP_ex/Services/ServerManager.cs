///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using YControls.Command;
using YFrameworkBase;

namespace WNMP_ex.Services {
    /// <summary>
    /// 服务器状态改变回调
    /// </summary>
    /// <param name="oldstatus"></param>
    /// <param name="newstatus"></param>
    public delegate void OnServerStatusChanged(ServerStatus oldstatus, ServerStatus newstatus);

    /// <summary>
    /// 服务器状态
    /// </summary>
    public enum ServerStatus {
        Stop = 000,
        MySQL = 0b001,
        NM = 0b011,
        Nginx = 0b010,
        NP = 0b110,
        PHP = 0b100,
        MP = 0b101,
        NMP = 0b111,
    }

    /// <summary>
    /// 服务器管理类
    /// </summary>
    public class ServerManager : SingletonBase<ServerManager> {
        #region Properties
        public event OnServerStatusChanged OnServerStatusChange;
        public event CommandAction OnButtonClick;

        public ServerStatus Status;

        private string _phproot {
            //@"C:\WNMP\PHP\php-7.2.16-nts\"
            get => WNMP_Setting.Instance.PathMap[Properties.Resources.PHP_ROOT];
        }
        private string _phpport {
            //"127.0.0.1:9000";
            get => WNMP_Setting.Instance.PathMap[Properties.Resources.PHP_PORT];
        }

        private string _nginxroot {
            //@"C:\WNMP\Nginx\nginx-1.15.10\";
            get => WNMP_Setting.Instance.PathMap[Properties.Resources.NGINX_ROOT];
        }

        private string _mysqlservicename {
            //"MySQL_Y";
            get => WNMP_Setting.Instance.PathMap[Properties.Resources.MYSQL_SERVER_NAME];
        }

        public CommandBase Command { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// 检测相关服务是否开启
        /// </summary>
        public bool TestServer(ServerStatus status) {
            switch (status) {
                case ServerStatus.MySQL:
                    return TestProcess("mysqld");
                case ServerStatus.Nginx:
                    return TestProcess("nginx");
                case ServerStatus.PHP:
                    return TestProcess("php-cgi");
                default: return false;
            }
        }

        private bool TestProcess(string name) {
            return Process.GetProcessesByName(name).Count() > 0;
        }

        /// <summary>
        /// 启动服务 假设MySQL作为服务一直开启，一般只需要启动NP
        /// </summary>
        /// <param name="status"></param>
        /// <param name="mode"></param>
        public void SwitchStatus(ServerStatus status, bool mode = true) {
            switch (status) {
                case ServerStatus.MySQL:
                    Status = mode ? StartMySql() : StopMySql();
                    break;
                case ServerStatus.Nginx:
                    Status = mode ? StartNginx() : StopNginx();
                    break;
                case ServerStatus.PHP:
                    Status = mode ? StartPHP() : StopPHP();
                    break;
                case ServerStatus.NP:
                    SwitchStatus(ServerStatus.Nginx, mode);
                    SwitchStatus(ServerStatus.PHP, mode);
                    break;
                case ServerStatus.NMP:
                    SwitchStatus(ServerStatus.Nginx, mode);
                    SwitchStatus(ServerStatus.PHP, mode);
                    SwitchStatus(ServerStatus.MySQL, mode);
                    break;
            }
        }

        private ServerStatus StartNginx() {
            RunCommand($"{_nginxroot}nginx.exe", $" -p {_nginxroot}");
            OnServerStatusChange?.Invoke(Status, Status | ServerStatus.Nginx);
            return Status | ServerStatus.Nginx;
        }

        private ServerStatus StopNginx() {
            RunCommand($"{_nginxroot}nginx.exe", $" -s stop -p {_nginxroot}");
            foreach (var proc in Process.GetProcessesByName("nginx")) {
                proc.Kill();
            }
            OnServerStatusChange?.Invoke(Status, Status & ~ServerStatus.Nginx);
            return Status & ~ServerStatus.Nginx;
        }

        private ServerStatus StartPHP() {
            RunCommand($"{_phproot}php-cgi.exe", $" -b {_phpport} -c {_phproot}php.ini");
            OnServerStatusChange?.Invoke(Status, Status | ServerStatus.PHP);
            return Status | ServerStatus.PHP;
        }

        private ServerStatus StopPHP() {
            foreach (var proc in Process.GetProcessesByName("php-cgi")) {
                proc.Kill();
            }
            OnServerStatusChange?.Invoke(Status, Status & ~ServerStatus.PHP);
            return Status & ~ServerStatus.PHP;
        }

        private ServerStatus StartMySql() {
            using (ServiceController ser = new ServiceController { ServiceName = _mysqlservicename }) {
                if (ser.Status != ServiceControllerStatus.Running && ser.Status != ServiceControllerStatus.StartPending)
                    ser.Start();
            }
            OnServerStatusChange?.Invoke(Status, Status | ServerStatus.MySQL);
            return Status | ServerStatus.MySQL;
        }

        private ServerStatus StopMySql() {
            using (ServiceController ser = new ServiceController { ServiceName = _mysqlservicename }) {
                if (ser.Status != ServiceControllerStatus.Stopped && ser.Status != ServiceControllerStatus.StopPending)
                    ser.Stop();
            }
            OnServerStatusChange?.Invoke(Status, Status & ~ServerStatus.MySQL);
            return Status & ~ServerStatus.MySQL;
        }

        private static void RunCommand(string filename, string command) {
            using (Process proc = new Process()) {
                proc.StartInfo = new ProcessStartInfo {
                    FileName = filename,
                    Arguments = command,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                };
                proc.Start();
            }
        }

        public void TurnServer(bool mode) {
            if (WNMP_Setting.Instance.Defaultstatus == ServerStatus.NP)
                SwitchStatus(ServerStatus.NP, mode);
            else
                SwitchStatus(ServerStatus.NMP, mode);
        }

        private void OnButtonCommand(object para) {
            OnButtonClick?.Invoke(para);
        }
        #endregion

        #region Constructors
        ~ServerManager() {
            TurnServer(false);
        }

        public ServerManager() {
            Command = new CommandBase(OnButtonCommand);
        }
        #endregion
    }
}
