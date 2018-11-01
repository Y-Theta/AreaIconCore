using System.Runtime.Serialization;

namespace AreaIconCore.Models {
    [DataContract]
    public class ExtensionInfo {
        #region Properties
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool Enabled { get; set; }
        #endregion

        #region Methods
        #endregion

        #region Constructors
        public ExtensionInfo(string name,bool enable) {
            Name = name;
            Enabled = enable;
        }
        #endregion
    }

}