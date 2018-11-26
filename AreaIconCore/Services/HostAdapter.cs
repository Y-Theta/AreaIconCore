using AreaIconCore.Models;
using ExtensionContract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using YFrameworkBase;

namespace AreaIconCore.Services {
    public class HostAdapter : SingletonBase<HostAdapter> ,INotifyPropertyChanged {
        #region Properties
        [ImportMany(typeof(IInnerDomainExtensionContract), AllowRecomposition = true)]
        private ObservableCollection<IInnerDomainExtensionContract> _addInContracts;
        public ObservableCollection<IInnerDomainExtensionContract> AddInContracts {
            get => _addInContracts;
            set => _addInContracts = value;
        }

        public Dictionary<string, IInnerDomainExtensionContract> ExtensionDirectory { get; set; }

        private String _pluginsPath;
        public String PluginsPath {
            get => _pluginsPath;
            set => InitPlugins(value);
        }

        private CompositionContainer _container;

        private AggregateCatalog _acceptcatalog;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        /// <summary>
        /// 初始化控件，装载在CoreSettings中为Enable的控件
        /// </summary>
        private void InitPlugins(string path) {
            _pluginsPath = path;
            _acceptcatalog = new AggregateCatalog();
            string[] pahts = Directory.GetDirectories(_pluginsPath);
            foreach (var p in pahts) {
                string key = p.Split('\\').Last();
                if (!CoreSettings.Instance.Extensions.ContainsKey(key) || CoreSettings.Instance.Extensions[key].Enabled) {
                    _acceptcatalog.Catalogs.Add(new DirectoryCatalog(p));
                    if (!CoreSettings.Instance.Extensions.ContainsKey(key)) {
                        CoreSettings.Instance.Extensions.Add(key, new ExtensionInfo { AssemblyName = key });
                    }
                }
            }
            _container = new CompositionContainer(_acceptcatalog);
            _container.ComposeParts(this);
            foreach (var extension in _addInContracts) {
                extension.Notify += Extension_Notify;
                string key = extension.GetType().Assembly.GetName().Name;
                if(CoreSettings.Instance.Extensions.ContainsKey(key)) {
                    CoreSettings.Instance.Extensions[key].Name = extension.Name;
                    CoreSettings.Instance.Extensions[key].Description = extension.Description;
                    CoreSettings.Instance.Extensions[key].Enabled = true;
                }
                PropertyChanged?.Invoke(CoreSettings.Instance, new PropertyChangedEventArgs("Extensions"));
                ExtensionDirectory.Add(extension.Name, extension);
            }
        }

        private object Extension_Notify(object sender, ApplicationScenario scenario, object obj) {
            switch (scenario) {
                case ApplicationScenario.AreaIcon:
                    return null;
                default: return null;
            }
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
