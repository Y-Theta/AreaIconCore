using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPGW_ex.Model {
    /// <summary>
    /// 流量包信息
    /// </summary>
    [DataContract]
    internal struct FlowPackage {
        #region Properties
        /// <summary>
        /// 价格
        /// </summary>
        [DataMember]
        public int Price;
        /// <summary>
        /// 流量
        /// </summary>
        [DataMember]
        public int Count;
        #endregion
    }
}
