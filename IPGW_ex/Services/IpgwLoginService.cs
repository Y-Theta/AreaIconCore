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
using YFrameworkBase;
using YFrameworkBase.DataAccessor;

namespace IPGW_ex.Services {
    internal class IpgwLoginService : SingletonBase<IpgwLoginService> {
        #region Properties
        private const string _webname = "NEU_Ipgw";
        private const string _loginuri = "login";
        private const string _datauri = "fluxdata";

        public LoginServices ServiceHolder { get; set; }

        protected WebInfoContainer InfoContainer {
            get => ServiceHolder.Container;
        }

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
            Task tsk = new Task(() => {
                FlowInfo info = GetLatestFlow();
                App.Current.Dispatcher.Invoke(() => {
                    IpgwSetting.Instance.LatestFlow = info;
                }, DispatcherPriority.Normal);
            });
            tsk.Start();
            await tsk;
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
                    ServiceHolder.Post(InfoContainer.BaseUri + InfoContainer.Uris[_loginuri], InfoContainer.KeyValuePairs);
                    String rest = ServiceHolder.GetString(InfoContainer.BaseUri + InfoContainer.Uris[_datauri], InfoContainer.Compressed);
                    if (rest is null) {
                        return "请确保物理网络已连接!";
                    }
                    else if (rest.Equals("not_online")) {
                        String ori = ServiceHolder.GetResponse();
                        if (ori.Contains("用户不存在"))
                            return "请输入正确的用户名!";
                        else if (ori.Contains("密码错误"))
                            return "密码错误!";
                        else
                            return "网关被占用,请先断开连接并重新登录!";
                    }
                    else {
                        ConnectState = true;
                        data = rest;
                        return "网络已连接.";
                    }
                }
                catch (AggregateException) {
                    return "连接超时!";
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
                ServiceHolder.Post(InfoContainer.BaseUri + InfoContainer.Uris[_loginuri], InfoContainer.KeyValuePairs, new Dictionary<string, string>{
                { "action","logout" }, });
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
