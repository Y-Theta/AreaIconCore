using ExtensionContract;
using IPGW_ex.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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

namespace IPGW_ex {
    [Export(typeof(IInnerDomainExtensionContract))]
    public class IPGW_ex_Main : InnerDomainExtenesion<IPGW_ex_Main> {

        public bool MainWindowBlur {
            get {
                return (bool)PostData(this, ApplicationScenario.BlurState);
            }
        }

        public override object Run(ApplicationScenario c, object arg = null) {
            switch (c) {
                case ApplicationScenario.AreaIcon:
                    return null;
                case ApplicationScenario.MainPage:
                    return new MainPage();
                case ApplicationScenario.AreaPopup:
                    return null;
                default:
                    return null;
            }
        }


        public IPGW_ex_Main() {
            Name = "IPGW_ex";
            Author = "Y_Theta";
            Edition = "1";
            Description = "用于IPGW的相关控件";
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
