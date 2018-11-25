using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YFrameworkBase.DataAccessor;

namespace IPGW_ex.Model {
    /// <summary>
    /// 流量类
    /// </summary>
    [XmlOperate(
        @"extensions\IPGW_ex\fluxdata.xml",
        nameof(Time),
        "Ipgw-fluxinfo",
        "item",
        "prop")]
    internal class FluxInfo {
        #region Properties
        /// <summary>
        /// 时间戳
        /// </summary>
        [XmlMember(typeof(DateTime), nameof(Time), XmlMemberType.UserElement,
            nameof(DataTime_Set), nameof(DataTime_Get))]
        public DateTime Time { get; set; }

        /// <summary>
        /// 当前流量值
        /// </summary>
        [XmlMember(typeof(double), nameof(Data), XmlMemberType.RootElement)]
        public double Data { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        [XmlMember(typeof(double), nameof(Balance), XmlMemberType.RootElement)]
        public double Balance { get; set; }
        #endregion

        #region Methods
        private void DataTime_Set(string key) {
            Time = DateTime.FromFileTime(long.Parse(key));
        }

        private string DataTime_Get() {
            return Time.ToFileTime().ToString();
        }
        #endregion

        #region
        public override string ToString() {
            return string.Format($"< " +
                $"TIME:{Time.ToFileTime().ToString().PadRight(20)}  " +
                $"data:{Data.ToString().PadRight(20)} " +
                $"balance:{Balance.ToString().PadRight(20)} >");
        }
        #endregion
    }
}
