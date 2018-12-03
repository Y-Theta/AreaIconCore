using ExtensionContract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_ex {
    [Export(typeof(InnerDomainExtenesion))]
    public class TodoCore : InnerDomainExtenesion {
        #region Properties
        #endregion

        #region Methods
        public override object Run(ApplicationScenario c, object arg = null) {
            throw new NotImplementedException();
        }
        #endregion

        #region Constructors
        public TodoCore() {
            Name = "计划控件";
            Author = "Y_Theta";
            Edition = "1";
            Description = "用于记录一些待办事项并在适当时提醒";
            Application = new Dictionary<ApplicationScenario, int> {
                { ApplicationScenario.MainWindowInit,1 },
            };
        }
        #endregion

    }

}
