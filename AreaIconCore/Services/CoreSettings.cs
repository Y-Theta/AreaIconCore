using AreaIconCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using YFrameworkBase.Setting;

namespace AreaIconCore.Services {
    [DataContract]
    public class CoreSettings : ILocalSetting {
        #region Properties
        private static CoreSettings _instence;
        private static readonly object _slock = new object();
        public static CoreSettings Instence {
            get {
                if (_instence is null) {
                    lock (_slock) {
                        if (_instence is null) {
                            _instence = new CoreSettings();
                        }
                    }
                }
                return _instence;
            }
        }

        [DataMember]
        private List<ExtensionInfo> _extensions;
        public List<ExtensionInfo> Extensions {
            get => _extensions;
            set => SetValue(out _extensions, value, Extensions);
        }
        #endregion

        #region Methods
        public override void LoadValue<T>( T setting) {
            if (setting is CoreSettings)
                _instence = setting as CoreSettings;
        }

        public void Init() {
            Extensions = new List<ExtensionInfo>();
        }
        #endregion

        #region Constructors

        public CoreSettings() { Init(); }
        #endregion
    }

}
