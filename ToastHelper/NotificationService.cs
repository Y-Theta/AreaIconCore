using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using static ToastHelper.NotificationActivator;

namespace ToastHelper {

    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(INotificationActivationCallback))]
    [Guid("9a88b91d-f9c4-4c63-91dd-175c2c2cb458"), ComVisible(true)]
    public class NotificationService : NotificationActivator {
        #region Methods
        public void Init(string appid) {
            DesktopNotificationManagerCompat.RegisterAumidAndComServer<NotificationService>(appid);
            DesktopNotificationManagerCompat.RegisterActivator<NotificationService>();
        }

        public override void OnActivated(string arguments, NotificationUserInput userInput, string appUserModelId) {

        }

        public void Notify(string title, string content) {
            XmlDocument xml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            XmlNodeList lines = xml.GetElementsByTagName("text");
            lines[0].AppendChild(xml.CreateTextNode(title));
            lines[1].AppendChild(xml.CreateTextNode(content));

            XmlElement binding = (XmlElement)xml.GetElementsByTagName("binding")[0];
            binding.SetAttribute("template", "ToastGeneric");

            ToastNotification toast = new ToastNotification(xml);
            DesktopNotificationManagerCompat.CreateToastNotifier().Show(toast);
        }

        public void ClearHistory(string appid) {
            new DesktopNotificationHistoryCompat(appid).Clear();
        }
        #endregion

        #region Constructors
        #endregion
    }

}
