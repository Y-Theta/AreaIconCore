using ExtensionContract;
using IPGW_ex.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using ColorD = System.Drawing.Color;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YControls.FlowControls;
using AreaIconCore.Services;
using IPGW_ex.Controls;
using System.Drawing;
using IPGW_ex.Services;

namespace IPGW_ex {
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(InnerDomainExtenesion))]
    public class IPGWCore : InnerDomainExtenesion {

        private static Lazy<IPGWCore> _instence = new Lazy<IPGWCore>();
        public static IPGWCore Instence {
            get => _instence.Value;
        }

        /// <summary>
        /// 主程序控制控件方法
        /// </summary>
        public override object Run(ApplicationScenario c, object arg = null) {
            switch (c) {
                case ApplicationScenario.AreaIcon:
                    return GetAreaIcon();
                case ApplicationScenario.MainPage:
                    return new MainPage();
                case ApplicationScenario.AreaPopup:
                    return new FlowPanel();
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取图标化字符
        /// </summary>
        public Icon GetAreaIcon() {
            if(string.IsNullOrEmpty(FormatService.Instance.LatestPercent))
                FormatService.Instance.LatestPercent = string.Format("{0}", FormatService.Instance.CastToPercent(IpgwSetting.Instance.LatestFlow, IpgwSetting.Instance.Package, false));
            return AreaIconDraw.Instance.StringIcon(FormatService.Instance.LatestPercent, IpgwSetting.Instance.IconFontColor, (float)IpgwSetting.Instance.IconFontSize);
        }

        /// <summary>
        /// 更新托盘图标
        /// </summary>
        public void UpdateAreaIcon() {
            base.PostData(this, ApplicationScenario.AreaIcon, GetAreaIcon());
        }

        /// <summary>
        /// 
        /// </summary>
        public object PostData(ApplicationScenario appc, object data = null) {
            return base.PostData(this, appc, data);
        }

        public IPGWCore() {
            Name = "IPGW控件";
            Author = "Y_Theta";
            Edition = "1";
            Description = "用于东北大学IPGW网关的登录和流量监控";
            Application = new Dictionary<ApplicationScenario, int> {
                { ApplicationScenario.AreaIcon ,1},
                { ApplicationScenario.AreaContextMenu ,1},
                { ApplicationScenario.AreaPopup ,1},
                { ApplicationScenario.MainPage ,1},
                { ApplicationScenario.MainWindowInit,1 },
            };
        }
    }
}
