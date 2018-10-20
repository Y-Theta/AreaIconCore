using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using YFrameworkBase.Setting;

namespace AreaIconCore.Services {
    public class CoreSettings : LocalSetting<CoreSettings> {
        #region Properties
        [DataMember]
        public List<String> Extensions { get; set; }
        #endregion

        #region Methods
        public override void Init() {
            FileName = AppDomain.CurrentDomain.FriendlyName.Split('.')[0] + "_setting.xml";
            CachePath = @"Settings\";
            Extensions = new List<string>() { "222", "333", "444" };
            base.Init();
        }
        #endregion

        #region Constructors
        #endregion
    }

}
