using IPGW_ex.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using SysConvert = System.Convert;
using YFrameworkBase;
using XmlDataProvider = YFrameworkBase.DataAccessor.XmlDataProvider;
using System.Windows;

namespace IPGW_ex.Services {
    /// <summary>
    /// 有关流量信息的格式化方法
    /// </summary>
    internal class FormatService : SingletonBase<FormatService>, IValueConverter {
        /// <summary>
        /// 流量进制
        /// </summary>
        private const double TICK = 1024.0;

        /// <summary>
        /// 最新的剩余流量百分比
        /// </summary>
        internal string LatestPercent;

        #region Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is null)
                return null;
            if (parameter is string) {
                switch (parameter) {
                    case "Percent_Use":
                        return string.Format("{0}", CastToPercent((FlowInfo)value, IpgwSetting.Instance.Package, true));
                    case "Percent_Rem":
                        LatestPercent = string.Format("{0}", CastToPercent((FlowInfo)value, IpgwSetting.Instance.Package, false));
                        return LatestPercent;
                    case "Used":
                        return CastToFit(GetUsedFlow((FlowInfo)value));
                    case "Balance":
                        return CastToFit(GetBalanceFlow((FlowInfo)value, IpgwSetting.Instance.Package));
                    case "Cost":
                        return string.Format("{0} 元", GetActualBalance((FlowInfo)value, IpgwSetting.Instance.Package));
                    case "SumCost":
                        return ((FlowInfo)value).Balance;
                    case "Span":
                        return GetSpan(IpgwSetting.Instance.LatestFlow);
                    case "Recharge":
                        return NeedRecharge() ? Visibility.Visible : Visibility.Collapsed;
                    case "State_Text":
                        return (bool)value ? "已连接" : "未连接";
                    case "State_Vis":
                        return (bool)value ? Visibility.Visible : Visibility.Collapsed;
                    case "User":
                        return IpgwLoginService.Instance.UserID;
                    default: return value;
                }
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断是否需要充值
        /// </summary>
        internal bool NeedRecharge() {
            return GetActualBalance(IpgwSetting.Instance.LatestFlow, IpgwSetting.Instance.Package) <= IpgwSetting.Instance.Package.Price;
        }

        /// <summary>
        /// 判断是否超过警戒值
        /// </summary>
        internal bool Warning() {
            return CastToPercent(IpgwSetting.Instance.LatestFlow, IpgwSetting.Instance.Package, false) <= IpgwSetting.Instance.WarnningLevel;
        }

        /// <summary>
        /// 流量信息百分比格式化
        /// </summary>
        /// <param name="info">流量信息</param>
        /// <param name="package">流量包类型</param>
        /// <param name="used">true 已使用/false 未使用</param>
        public double CastToPercent(FlowInfo info, FlowPackage package, bool used) {
            double ret = 0;
            double balance = GetBalanceFlow(info, package);
            double use = GetUsedFlow(info);
            if (used)
                ret = use / (use + balance);
            else
                ret = balance / (use + balance);
            return Math.Floor(ret * 100);
        }

        /// <summary>
        /// 获取已使用流量 /MB
        /// </summary>
        /// <param name="info">流量信息</param>
        private double GetUsedFlow(FlowInfo info) {
            return info.Data;
        }

        /// <summary>
        /// 获取剩余流量 /MB
        /// </summary>
        /// <param name="info">流量信息</param>
        private double GetBalanceFlow(FlowInfo info, FlowPackage package) {
            double packageflow = package.Count * TICK;
            double actbalance = info.Balance - package.Price;
            if (info.Data <= packageflow)
                return packageflow - info.Data + actbalance * TICK;
            else
                return actbalance * TICK;
        }

        /// <summary>
        /// 将流量值转化到合适的单位
        /// </summary>
        private string CastToFit(double data) {
            if (data < 1024)
                return string.Format("{0:##.##} M", data);
            else
                return string.Format("{0:##.##} G", data / TICK);
        }

        /// <summary>
        /// 由IPGW返回的字符串格式化
        /// </summary>
        internal FlowInfo GetIpgwDataInf(string data) {
            FlowInfo info = new FlowInfo();
            try {
                string[] t = data.Split(new char[] { ',' });
                info.Data = FluxFormater(t[0]);
                info.Balance = SysConvert.ToDouble(t[2]);
                info.Time = DateTime.Now;
                //TODO::计算时间差
            }
            catch {
                return null;
            }
            //TODO::保存本地
            XmlDataProvider.Instance.AddNode(info);
            return info;
        }

        /// <summary>
        /// 获取账户余额
        /// </summary>
        private double GetActualBalance(FlowInfo info, FlowPackage package) {
            return info.Balance - package.Price;
        }

        /// <summary>
        /// 获取距上次刷新时间
        /// </summary>
        private string GetSpan(FlowInfo info) {
            TimeSpan span = DateTime.Now.Subtract(info.Time);
            if (span.TotalSeconds > 60) {
                if (span.TotalMinutes > 60) {
                    return string.Format("{0:#} H", span.TotalHours);
                }
                else {
                    return string.Format("{0:#} M", span.TotalMinutes);
                }
            }
            else
                return string.Format("{0:#} S", span.TotalSeconds);
        }

        /// <summary>
        /// 流量信息格式化 in(B) out(M)
        /// </summary>
        private double FluxFormater(string flux) {
            try {
                Int64 a = SysConvert.ToInt64(flux);
                return a / TICK / TICK;
            }
            catch (FormatException) {
                return 0;
            }
        }
        #endregion
    }
}
