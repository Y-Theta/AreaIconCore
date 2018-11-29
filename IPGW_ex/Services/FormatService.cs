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

namespace IPGW_ex.Services {
    /// <summary>
    /// 有关流量信息的格式化方法
    /// </summary>
    public class FormatService : SingletonBase<FormatService>, IValueConverter {

        #region Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            switch (parameter) {
                case "Percent":
                    return value;
                case "Used":
                    return value;
                case "Balance":
                    return value;
                case "Cost":
                    return value;
                case "SumCost":
                    return value;
                case "Span":
                    return value;
                default: return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 流量信息百分比格式化
        /// </summary>
        public double CastToPercent(FlowInfo info, int package, bool used) {
            double ret = 0;


            return ret;
        }

        /// <summary>
        /// 由IPGW返回的字符串格式化
        /// </summary>
        private FlowInfo GetIpgwDataInf(string data) {
            FlowInfo info = new FlowInfo();
            try {
                string[] t = data.Split(new char[] { ',' });
                info.Data = FluxFormater(t[0]);
                info.Balance = SysConvert.ToDouble(t[2]);
                info.Time = DateTime.Now;
                //TODO::计算时间差
            }
            catch (IndexOutOfRangeException) {
                return null;
            }
            catch (NullReferenceException) {
                return null;
            }
            //TODO::保存本地
            return info;
        }

        /// <summary>
        /// 流量信息格式化 in(Byte)
        /// </summary>
        /// <param name="flux"></param>
        /// <returns></returns>
        private double FluxFormater(string flux) {
            try {
                Int64 a = SysConvert.ToInt64(flux);
                return a / 1000000.0;
            }
            catch (FormatException) {
                return 0;
            }
        }
        #endregion
    }

}
