using AreaIconCore.Models;
using ExtensionContract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
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
using WNMP_ex.Controls;
using WNMP_ex.Services;

namespace WNMP_ex {

    [Export(typeof(InnerDomainExtenesion))]
    public class WNMPCore : InnerDomainExtenesion {

        public override object Run(ApplicationScenario c, object arg = null) {
            switch (c) {
                case ApplicationScenario.AreaIcon:
                    return new Icon(ConstTable.AppIcon);
                case ApplicationScenario.MainPage:
                    return null;
                case ApplicationScenario.SessionEnding:
                    ServerManager.Instance.TurnServer(false) ;
                    return null;
                case ApplicationScenario.AreaPopup:
                    return new ServerPanel();
                default:
                    return null;
            }
        }

        public WNMPCore() {
            Name = "WNMP控件";
            Author = "Y_Theta";
            Edition = "1";
            Description = "Windows下NMP集成控制模块";
            Application = new Dictionary<ApplicationScenario, int> {
                { ApplicationScenario.AreaIcon ,1},
                { ApplicationScenario.AreaContextMenu ,1},
                { ApplicationScenario.AreaPopup ,1},
                { ApplicationScenario.MainWindowInit,1},
                { ApplicationScenario.SessionEnding,1},
            };
        }
    }
}
