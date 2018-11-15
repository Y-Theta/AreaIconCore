using ExtensionContract;
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

namespace IPGW_ex {
    [Export(typeof(IInnerDomainExtensionContract))]
    public class IPGW_ex : InnerDomainExtenesion<IPGW_ex> {
        public override object Run(ApplicationScenario c, object arg = null) {
            throw new NotImplementedException();
        }


        public IPGW_ex() {
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
