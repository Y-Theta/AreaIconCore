using IPGW_ex.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ColorD = System.Drawing.Color;
using YFrameworkBase.Setting;

namespace IPGW_ex.Services {
    [SettingFile("settings", "ipgw_set.xml")]
    [DataContract]
    internal class IpgwSetting :ILocalSetting<IpgwSetting> {
        #region Properties
        /// <summary>
        /// 当前流量信息
        /// </summary>
        [DataMember]
        private FlowInfo _latestflow;
        public FlowInfo LatestFlow {
            get => _latestflow;
            set => SetValue(out _latestflow, value, LatestFlow);
        }

        /// <summary>
        /// 流量套餐
        /// </summary>
        [DataMember]
        private FlowPackage _package;
        public FlowPackage Package {
            get => _package;
            set => SetValue(out _package, value, Package);
        }

        /// <summary>
        /// 流量警戒值
        /// </summary>
        [DataMember]
        private int _warnninglevel;
        public int WarnningLevel {
            get => _warnninglevel;
            set => SetValue(out _warnninglevel, value, WarnningLevel);
        }

        /// <summary>
        /// 托盘图标字体大小
        /// </summary>
        [DataMember]
        private double _iconfontsize;
        public double IconFontSize {
            get => _iconfontsize;
            set => SetValue(out _iconfontsize, value, IconFontSize);
        }

        /// <summary>
        /// 托盘图标的字体颜色
        /// </summary>
        [DataMember]
        private ColorD _iconfontcolor;
        public ColorD IconFontColor {
            get => _iconfontcolor;
            set => SetValue(out _iconfontcolor, value, IconFontColor);
        }
        #endregion

        #region Methods
        protected void Init() {
            LatestFlow = new FlowInfo { Time = DateTime.Now, Data = 0, Balance = 0 };
            Package = new FlowPackage { Price = 20, Count = 60 };
            IconFontColor = ColorD.FromArgb(255, 255, 255, 255);
            IconFontSize = 6;
        }
        #endregion

        #region Constructors
        public IpgwSetting() { Init(); }
        #endregion
    }

}
