using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AreaIconCore.Services {
    public class InnerProcessAssemblyManager {
        #region Properties
        public List<string> Directorys { get; set; }
        #endregion

        #region Methods
        public void HockResolve(AppDomain dom) {
            dom.AssemblyResolve += MyResolveEventHandler;
        }

        private Assembly MyResolveEventHandler(object sender, ResolveEventArgs args) {
            Assembly MyAssembly;
            String MisAssembly = "";
            if (args.Name.Contains(".resources")) {
                return null;
            }
            foreach (var dir in Directorys) {
                string[] items = Directory.GetFileSystemEntries(dir, args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");
                if (items.Count() > 0) {
                    MisAssembly = items.First();
                    break;
                }
            }
            MyAssembly = Assembly.LoadFrom(MisAssembly);

            return MyAssembly;
        }
        #endregion

        #region Constructors
        public InnerProcessAssemblyManager() {
            Directorys = new List<string>();
        }
        #endregion
    }

}
