using AreaIconCore.Models;
using AreaIconCore.Views;
using ExtensionContract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Drawing;
using System.IO;
using System.Linq;
using YFrameworkBase;

namespace AreaIconCore.Services {
    public class HostAdapter : SingletonBase<HostAdapter> ,INotifyPropertyChanged {
        #region Properties
        [ImportMany(typeof(InnerDomainExtenesion))]
        private ObservableCollection<InnerDomainExtenesion> _addInContracts;
        public ObservableCollection<InnerDomainExtenesion> AddInContracts {
            get => _addInContracts;
            set => _addInContracts = value;
        }

        public Dictionary<string, InnerDomainExtenesion> ExtensionDirectory { get; set; }

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
                string key = extension.GetType().Assembly.GetName().Name;
                if(CoreSettings.Instance.Extensions.ContainsKey(key)) {
                    CoreSettings.Instance.Extensions[key].Name = extension.Name;
                    CoreSettings.Instance.Extensions[key].Description = extension.Description;
                    CoreSettings.Instance.Extensions[key].Edition = extension.Edition;
                    CoreSettings.Instance.Extensions[key].Enabled = true;
                }
                PropertyChanged?.Invoke(CoreSettings.Instance, new PropertyChangedEventArgs("Extensions"));
                ExtensionDirectory.Add(extension.Name, extension);
                InnerDomainExtenesion.Notify += Extension_Notify;
            }
        }

        private object Extension_Notify(object sender, ApplicationScenario scenario, object obj) {
            IInnerDomainExtensionContract invoker = sender as IInnerDomainExtensionContract;
            MainWindow wind = App.Current.MainWindow as MainWindow;
            switch (scenario) {
                case ApplicationScenario.AreaIcon:
                    wind.AreaIcons[invoker.Name].Areaicon = (Icon)obj;
                    return null;
                default: return null;
            }
        }

        private void Init() {
            ExtensionDirectory = new Dictionary<string, InnerDomainExtenesion>();
        }
        #endregion

        #region Constructors
        public HostAdapter() {
            Init();
        }
        #endregion
    }

}
