using ExtensionContract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Diagnostics;
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
    [Export(typeof(InnerDomainExtenesion))]
    public class D6Core : InnerDomainExtenesion {

        public override object Run(ApplicationScenario c, object obj) {
            switch (c) {
                case ApplicationScenario.AreaIcon:
                case ApplicationScenario.UnLoad:
                    PostData(this, ApplicationScenario.UnLoad);
                    return null;
                case ApplicationScenario.MainPage:
                    if (obj.ToString() == "Uri")
                        return new Uri(@"pack://application:,,,/D6_ex;component/PageAndControl/FluxPage.xaml");
                    return new FluxPage();
                default: return null;
            }
        }

        public D6Core() {
            Name = "Neu6";
            Author = "Y_Theta";
            Edition = "1";
            Description = "用于六维空间的流量查询/计算的相关控件";
            Application = new Dictionary<ApplicationScenario, int> {

            };
        }
    }
}
