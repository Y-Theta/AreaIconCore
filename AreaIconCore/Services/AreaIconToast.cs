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
    [Guid("ad020b22-7654-472e-a172-C173E6ADF0C3"), ComVisible(true)]
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
