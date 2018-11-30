using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using YFrameworkBase.DataAccessor;

namespace IPGW_ex.Model {
    /// <summary>
    /// 流量类
    /// </summary>
    [XmlOperate(
        @"extensions\IPGW_ex\log_ipgwflow.xml",
        nameof(Time),
        "Log",
        "item",
        "prop",
        nameof(DataTime_Equal))]
    [DataContract]
    internal class FlowInfo {
        #region Properties
        /// <summary>
        /// 时间戳     /Y/M/D/H/M
        /// </summary>
        [DataMember]
        [XmlMember(typeof(DateTime), nameof(Time), XmlMemberType.UserElement,
            nameof(DataTime_Set), nameof(DataTime_Get))]
        public DateTime Time { get; set; }

        /// <summary>
        /// 当前流量值 /MB
        /// </summary>
        [DataMember]
        [XmlMember(typeof(double), nameof(Data), XmlMemberType.RootElement)]
        public double Data { get; set; }

        /// <summary>
        /// 账户余额  /元
        /// </summary>
        [DataMember]
        [XmlMember(typeof(double), nameof(Balance), XmlMemberType.RootElement)]
        public double Balance { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// 主键比较精确到天
        /// </summary>
        private bool DataTime_Equal(DateTime value, string test) {
            string day = Time.ToShortDateString();
            string[] testday = test.Split('/');
            return day.Equals(testday[0] + "/" + testday[1] + "/" + testday[2]);
        }

        private void DataTime_Set(string key) {
            string[] args = key.Split('/');
            Time = new DateTime(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]), int.Parse(args[3]), int.Parse(args[4]), 0);
        }

        private string DataTime_Get() {
            string day = Time.ToShortDateString();
            string[] time = Time.ToShortTimeString().Split(':');
            return day + "/" + time[0] + "/" + time[1];
        }
        #endregion

        #region
        public override string ToString() {
            return string.Format($"< " +
                $"TIME:{DataTime_Get().PadRight(20)}  " +
                $"data:{Data.ToString().PadRight(20)} " +
                $"balance:{Balance.ToString().PadRight(20)} >");
        }
        #endregion
    }
}
