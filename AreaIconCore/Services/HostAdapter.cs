using ExtensionContract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YFrameworkBase;

namespace AreaIconCore.Services {
    public class HostAdapter : SingletonBase<HostAdapter> {
        #region Properties
        [ImportMany(typeof(IInnerDomainExtensionContract), AllowRecomposition = true)]
        private List<IInnerDomainExtensionContract> _addInContracts;
        public List<IInnerDomainExtensionContract> AddInContracts {
            get => _addInContracts;
            set => _addInContracts = value;
        }

        private String _pluginsPath;
        public String PluginsPath {
            get => _pluginsPath;
            set => InitPlugins(value);
        }

        private CompositionContainer _container;

        private DirectoryCatalog _catalog;
        #endregion

        #region Methods
        private void InitPlugins(string path) {
            _pluginsPath = path;
            AggregateCatalog agcatalog = new AggregateCatalog();
            string[] pahts = Directory.GetDirectories(_pluginsPath);
            foreach (var p in pahts) {
                string key = p.Split('\\').Last();
                if (!CoreSettings.Instence.Extensions.ContainsKey(key) || CoreSettings.Instence.Extensions[key]) 
                    agcatalog.Catalogs.Add(new DirectoryCatalog(p));
            }
            _container = new CompositionContainer(agcatalog);
        }


        #endregion

        #region Constructors
        #endregion
    }

}
