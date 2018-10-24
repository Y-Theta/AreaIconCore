using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YFrameworkBase;

namespace ExtensionContract {
    public abstract class InnerDomainExtenesion<T> : IInnerDomainExtensionContract where T : class, new(){
        #region Properties

        private static Lazy<T> _instence = new Lazy<T>(() => { return new T(); });
        public static T Instence {
            get => _instence.Value;
        }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Edition { get; set; }

        public string Description { get; set; }

        public Dictionary<ApplicationScenario, int> Application { get; set; }

        public event OnExtensionAction Notify;
        #endregion

        #region Methods
        public string InfoStringFormat() {
            return String.Format($"Name        :  {Name}\n" +
                                $"Author      :  {Author}\n" +
                                $"Edition     :  {Edition}\n" +
                                $"Description :  {Description}\n");
        }

        public void PostData(object sender, ApplicationScenario scenario) {
            Notify?.Invoke(sender, scenario);
        }

        public abstract object Run(ApplicationScenario c);
        #endregion

        #region Constructors
        #endregion

    }
}
