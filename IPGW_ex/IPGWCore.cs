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

namespace IPGW_ex {
    [Export(typeof(IInnerDomainExtensionContract))]
    public class IPGWCore : InnerDomainExtenesion<IPGWCore> {

        public bool MainWindowBlur {
            get {
                return (bool)PostData(this, ApplicationScenario.BlurState);
            }
        }

        public override object Run(ApplicationScenario c, object arg = null) {
            switch (c) {
                case ApplicationScenario.AreaIcon:
                    return AreaIconDraw.Instance.StringIcon("100", ColorD.AliceBlue, 4);
                case ApplicationScenario.MainPage:
                    return new MainPage();
                case ApplicationScenario.AreaPopup:
                    return new FlowPanel();
                default:
                    return null;
            }
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
