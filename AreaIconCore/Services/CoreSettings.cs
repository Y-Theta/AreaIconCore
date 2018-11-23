﻿using AreaIconCore.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using YFrameworkBase.Setting;

namespace AreaIconCore.Services {
    [SettingFile("settings", "core_set.xml")]
    [DataContract]
    public class CoreSettings : ILocalSetting<CoreSettings> {
        #region Properties

        [DataMember]
        private Dictionary<String, bool> _extensions;
        public Dictionary<String, bool> Extensions {
            get => _extensions;
            set => SetValue(out _extensions, value, Extensions);
        }

        [DataMember]
        private bool _enableBlur;
        public bool EnableBlur {
            get => _enableBlur;
            set => SetValue(out _enableBlur, value, EnableBlur);
        }

        [DataMember]
        private double _mainOpacity;
        public double MainOpacity {
            get => _mainOpacity;
            set => SetValue(out _mainOpacity, value, MainOpacity);
        }
        #endregion

        #region Methods
        public void Init() {
            Extensions = new Dictionary<String, bool>();
            MainOpacity = 1.0;
        }
        #endregion

        #region Constructors

        public CoreSettings() { Init(); }
        #endregion
    }

}
