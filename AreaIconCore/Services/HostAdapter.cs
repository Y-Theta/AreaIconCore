using ExtensionContract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using YFrameworkBase;

namespace AreaIconCore.Services {
    public class HostAdapter : SingletonBase<HostAdapter> {
        #region Properties
        [ImportMany(typeof(IInnerDomainExtensionContract), AllowRecomposition = true)]
        private List<IInnerDomainExtensionContract> _addInContracts;
        public Dictionary<string, IInnerDomainExtensionContract> ExtensionDirectory {  get; set; }

        private String _pluginsPath;
        public String PluginsPath {
            get => _pluginsPath;
            set => InitPlugins(value);
        }

        private CompositionContainer _container;

        private AggregateCatalog _acceptcatalog;
        #endregion

        #region Methods
        private void InitPlugins(string path) {
            _pluginsPath = path;
            _acceptcatalog = new AggregateCatalog();
            string[] pahts = Directory.GetDirectories(_pluginsPath);
            foreach (var p in pahts) {
                string key = p.Split('\\').Last();
                if (!CoreSettings.Instence.Extensions.ContainsKey(key) || CoreSettings.Instence.Extensions[key])
                    _acceptcatalog.Catalogs.Add(new DirectoryCatalog(p));
            }
            _container = new CompositionContainer(_acceptcatalog);
            _container.ComposeParts(this);
            foreach (var extension in _addInContracts) {
                extension.Notify += Extension_Notify;
                ExtensionDirectory.Add(extension.Name, extension);
            }
        }

        private void Extension_Notify(object sender, ApplicationScenario scenario) {
            Console.WriteLine(sender.ToString() + scenario);
        }

        private void Init() {
            ExtensionDirectory = new Dictionary<string, IInnerDomainExtensionContract>();
        }

        #endregion

        #region Constructors
        public HostAdapter() {
            Init();
        }
        #endregion
    }

}
