using AreaIconCore;
using AreaIconCore.Services;
using HttpServices;
using HttpServices.DataProviders;
using IPGW_ex.Model;
using IPGW_ex.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using YFrameworkBase.Setting;
using XmlDataProvider = YFrameworkBase.DataAccessor.XmlDataProvider;
using YFrameworkBase;
using System.Text.RegularExpressions;

namespace IPGW_ex.Services {
    internal class IpgwLoginService : SingletonBase<IpgwLoginService> {
        #region Properties
        private const string _webname = "NEU_Ipgw";
        private const string _loginuri = "login";
        private const string _logouturi = "logout";
        private const string _datauri = "fluxdata";

        public LoginServices ServiceHolder { get; set; }

        /// <summary>
        /// 用于网络连接的类
        /// </summary>
        protected WebInfoContainer InfoContainer {
            get => ServiceHolder.Container;
        }

        /// <summary>
        /// 当前账户
        /// </summary>
        internal string UserID {
            get => ServiceHolder.Container.KeyValuePairs.FirstOrDefault(f => { return f.Key.Equals("username"); }).Value;
        }

        /// <summary>
        /// 当前连接状态
        /// </summary>
        public bool ConnectState {
            get => FlowInfoViewModel.Singleton.ConnectState;
            set {
                App.Current.Dispatcher.Invoke(() => {
                    FlowInfoViewModel.Singleton.ConnectState = value;
                }, DispatcherPriority.Normal);
            }
        }
        #endregion

        #region Method

        /// <summary>
        /// 异步刷新数据
        /// </summary>
        public async Task Update(Action finishaction = null) {
            await Task.Run(() => {
                FlowInfo info = GetLatestFlow();
                App.Current.Dispatcher.Invoke(() => {
                    IpgwSetting.Instance.LatestFlow = info;
                }, DispatcherPriority.Normal);
                SettingManager.Instance.SaveSetting<IpgwSetting>();
            });
            App.Current.Dispatcher.Invoke(() => {
                finishaction?.Invoke();
            }, DispatcherPriority.Normal);

        }

        /// <summary>
        /// 获取最新流量信息
        /// </summary>
        internal FlowInfo GetLatestFlow() {
            string data = null;
            string msg = TestConnect(ref data);
            App.Current.Dispatcher.Invoke(() => {
                PopupMessageManager.Instance.Message(msg);
            }, DispatcherPriority.Normal);
            return FormatService.Instance.GetIpgwDataInf(data);
        }

        /// <summary>
        /// 仅检测网络通断
        /// </summary>
        public async Task TestOnly() {
            if (ServiceHolder.RefrashinfSet(_webname)) {
                Task tsk = new Task(() => {
                    string rest = "";
                    try { rest = ServiceHolder.GetString(InfoContainer.BaseUri + InfoContainer.Uris[_datauri], InfoContainer.Compressed); }
                    catch (AggregateException) {
                        App.Current.Dispatcher.Invoke(() => {
                            PopupMessageManager.Instance.Message("无Internet!");
                        }, DispatcherPriority.Send);
                        return;
                    }
                    if (string.IsNullOrEmpty(rest)) {
                        ConnectState = false;
                        return;
                    }
                    else if (rest.Equals("not_online"))
                        ConnectState = false;
                    else
                        ConnectState = true;
                });
                tsk.Start();
                await tsk;
            }
        }

        /// <summary>
        /// 检测并连接
        /// </summary>
        private string TestConnect(ref string data) {
            ConnectState = false;
            if (ServiceHolder.RefrashinfSet(_webname)) {
                try {
                    string result = ServiceHolder.GetString(InfoContainer.Uris[_loginuri], InfoContainer.Compressed);

                    var regexs = Regex.Matches(result, "<input type=\"hidden\".*name=\".*\" value=\"(.*)\" />");
                    string lt = Regex.Match(regexs[0].Value, "value=\"(.+)\"").Groups[1].Value;
                    string execution = Regex.Match(regexs[1].Value, "value=\"(.+)\"").Groups[1].Value;
                    string password = "", username = "";
                    foreach (var kv in ServiceHolder.Container.KeyValuePairs) {
                        if (kv.Key.Equals("password")) password = kv.Value;
                        else if (kv.Key.Equals("username")) username = kv.Value;
                    }

                    string posted = ServiceHolder.PostAsync(InfoContainer.Uris[_loginuri], new List<KeyValuePair<string, string>> {
                            new KeyValuePair<string, string>("ul",$"{username.Length}"),
                            new KeyValuePair<string, string>("pl",$"{password.Length}"),
                            new KeyValuePair<string, string>("lt",lt),
                            new KeyValuePair<string, string>("execution",execution),
                            new KeyValuePair<string, string>("_eventId","submit"),
                            new KeyValuePair<string, string>("rsa",username+password+lt),
                        }).Result;

                    string error = Regex.Match(posted, "id=\"errormsg\".*>(.*)<").Groups[1].Value;
                    string flux = ServiceHolder.GetString(InfoContainer.Uris[_datauri], InfoContainer.Compressed);
                    if (error.Equals("账号不存在！")) {
                        return "请输入正确的用户名!";
                    }
                    else {
                        ConnectState = true;
                        data = flux;
                        return "网络已连接.";
                    }
                }
                catch (AggregateException) {
                    return "连接超时!";
                }
                catch (Exception) {
                    return "未知错误";
                }
            }
            else
                return "请完善登录信息!";
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Logout() {
            if (ServiceHolder.RefrashinfSet(_webname)) {
                ServiceHolder.GetString(InfoContainer.Uris[_logouturi], InfoContainer.Compressed);
                ConnectState = false;
                PopupMessageManager.Instance.Message("网络已断开.");
            }
            else
                PopupMessageManager.Instance.Message("请完善登录信息!");
        }
        #endregion

        #region Constructors
        public IpgwLoginService() {
            ServiceHolder = LoginServices.Instance;
            ServiceHolder.DataProvider = new WebInfoProvider(XmlDataProvider.Instance);
            ServiceHolder.SetContainer(_webname);
        }
        #endregion
    }

}
