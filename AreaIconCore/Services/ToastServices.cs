///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ToastCore.Notification;

namespace AreaIconCore.Services {
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(INotificationActivationCallback))]
    [Guid("7ddba60f-e2f0-4373-8098-0eafb79ba54a"), ComVisible(true)]
    public class ToastServices : NotificationService {
        #region Properties
        #endregion

        #region Methods
        protected override void OnSetNotifyXML(string xml) {
            Console.WriteLine(xml);
            base.OnSetNotifyXML(xml);
        }
        #endregion

        #region Constructors
        #endregion
    }
}
