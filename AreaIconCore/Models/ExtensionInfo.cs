using System.Runtime.Serialization;

namespace AreaIconCore.Models {
    /// <summary>
    /// 插件信息类，用于插件离线时加载
    /// </summary>
    [DataContract]
    public class ExtensionInfo {
        #region Properties
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string AssemblyName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool Enabled { get; set; }
        #endregion

    }
}