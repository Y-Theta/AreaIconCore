using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ToastHelper;

namespace AreaIconCore.Services {
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(INotificationActivationCallback))]
    [Guid("F17B54EF-29FA-47F3-81B8-B47E434FE968"), ComVisible(true)]
    public class AreaIconToast : NotificationActivator {

        #region Methods
        public override void OnActivated(string arguments, NotificationUserInput userInput, string appUserModelId) {
            Console.WriteLine(arguments);
            Console.WriteLine(appUserModelId);
            Console.WriteLine(userInput.Count);
        }
        #endregion

    }

}
