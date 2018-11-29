using AreaIconCore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using YFrameworkBase.RegOperator;
using YFrameworkBase.Setting;

namespace AreaIconCore.Services {
    [SettingFile("settings", "core_set.xml")]
    [DataContract]
    public class CoreSettings : ILocalSetting<CoreSettings> {
        #region Properties
        /// <summary>
        /// 插件列表
        /// </summary>
        [DataMember]
        private Dictionary<String, ExtensionInfo> _extensions;
        public Dictionary<String, ExtensionInfo> Extensions {
            get => _extensions;
            set => SetValue(out _extensions, value, Extensions);
        }

        /// <summary>
        /// 是否启用毛玻璃效果
        /// </summary>
        [DataMember]
        private bool _enableBlur;
        public bool EnableBlur {
            get => _enableBlur;
            set => SetValue(out _enableBlur, value, EnableBlur);
        }

        /// <summary>
        /// 主窗口透明度
        /// </summary>
        [DataMember]
        private double _mainOpacity;
        public double MainOpacity {
            get => _mainOpacity;
            set => SetValue(out _mainOpacity, value, MainOpacity);
        }

        /// <summary>
        /// 主题
        /// </summary>
        [DataMember]
        private string _maintheme;
        public string MainTheme {
            get => _maintheme;
            set => SetValue(out _maintheme, value, MainTheme);
        }

        /// <summary>
        /// 语言
        /// </summary>
        [DataMember]
        private string _mainlang;
        public string MainLang {
            get => _mainlang;
            set => SetValue(out _mainlang, value, MainLang);
        }

        /// <summary>
        /// 开机自启动
        /// </summary>
        [DataMember]
        private bool _autorun;
        public bool AutoRun {
            get => _autorun;
            set => SetValue(out _autorun, value, AutoRun);
        }
        #endregion

        #region Methods
        public void Init() {
            Extensions = new Dictionary<String, ExtensionInfo>();
            MainOpacity = 1.0;
            AutoRun = true;
            MainTheme = AppearanceManager.__DefaultTheme;
            MainLang = AppearanceManager.__DefaultLanguage;
        }
        #endregion

        #region Constructors

        public CoreSettings() { Init(); }
        #endregion
    }

}
