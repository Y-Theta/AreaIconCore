using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YFrameworkBase;

namespace ExtensionContract {
    public abstract class InnerDomainExtenesion : IInnerDomainExtensionContract {
        #region Properties

        public string Name { get; set; }

        public string Author { get; set; }

        public string Edition { get; set; }

        public string Description { get; set; }

        public Dictionary<ApplicationScenario, int> Application { get; set; }

        /// <summary>
        /// 由于在域内,采用统一的事件来进行信息交换
        /// </summary>
        public static event OnExtensionAction Notify;
        #endregion

        #region Methods
        public string InfoStringFormat() {
            return String.Format($"Name        :  {Name}\n" +
                                $"Author      :  {Author}\n" +
                                $"Edition     :  {Edition}\n" +
                                $"Description :  {Description}\n");
        }

        public object PostData(object sender, ApplicationScenario scenario, object para = null) {
            var result = Notify?.Invoke(sender, scenario, para);
            return result;
        }

        public abstract object Run(ApplicationScenario c, object arg = null);
        #endregion
    }
}
