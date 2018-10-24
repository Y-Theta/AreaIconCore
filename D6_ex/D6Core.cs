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
using YFrameworkBase;

namespace D6_ex {
    [Export(typeof(IInnerDomainExtensionContract))]
    public class D6Core : InnerDomainExtenesion<D6Core> {

        public override object Run(ApplicationScenario c) {
            switch (c) {
                case ApplicationScenario.AreaIcon:
                    return null;
                default: return null;
            }
        }

        public D6Core() {
            Name = "D6_ex";
            Author = "Y_Theta";
            Edition = "1";
            Description = "用于六维空间的相关控件";
        }
    }
}
