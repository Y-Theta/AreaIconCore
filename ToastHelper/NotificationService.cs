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

        /// <summary>
        /// 发送一条通知 （标题/文本）
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">文本</param>
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

        /// <summary>
        /// 发送一条通知 （自定义图标/标题/文本）
        /// </summary>
        /// <param name="picuri">自定义图标路径</param>
        /// <param name="title">标题</param>
        /// <param name="content">文本</param>
        public void Notify(string picuri, string title, string content) {
            XmlDocument xml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            XmlElement binding = (XmlElement)xml.GetElementsByTagName("binding")[0];
            binding.SetAttribute("template", "ToastGeneric");

            XmlNodeList lines = xml.GetElementsByTagName("text");
            lines[0].AppendChild(xml.CreateTextNode(title));
            lines[1].AppendChild(xml.CreateTextNode(content));

            XmlElement image = xml.CreateElement("image");
            image.SetAttribute("placement", "appLogoOverride");
            image.SetAttribute("src", picuri);

            binding.AppendChild(image);

            ToastNotification toast = new ToastNotification(xml);
            DesktopNotificationManagerCompat.CreateToastNotifier().Show(toast);
        }

        /// <summary>
        /// 清除对应App的所有通知
        /// </summary>
        /// <param name="appid">app标识</param>
        public void ClearHistory(string appid) {
            new DesktopNotificationHistoryCompat(appid).Clear();
        }
        #endregion

        #region Constructors
        #endregion
    }

}
