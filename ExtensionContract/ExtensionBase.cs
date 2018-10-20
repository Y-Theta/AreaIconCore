using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionContract {
    public abstract class InnerDomainExtensionBase : IInnerDomainExtensionContract {
        #region Properties

        public string Name { get; set; }

        public string Author { get; set; }

        public string Edition { get; set; }

        public string Description { get; set; }

        public Dictionary<ApplicationScenario, int> Application { get; set; }

        public event OnExtensionAction Notify;
        #endregion

        #region Methods
        public virtual string InfoStringFormat() {
            return String.Format($"Name        :  {Name}\n" +
                                 $"Author      :  {Author}\n" +
                                 $"Edition     :  {Edition}\n" +
                                 $"Description :  {Description}\n");
        }

        public void PostData(object sender, ApplicationScenario scenario) {
            Notify?.Invoke(sender, scenario);
        }

        public virtual object Run(ApplicationScenario c) {
            return null;
        }
        #endregion

        #region Constructors
        #endregion
    }
}