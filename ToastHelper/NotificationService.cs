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

    /// <summary>
    /// Win10通知
    /// Toast通知回调
    /// </summary>
    /// <param name="arg">写在ToastCommands中的Argument</param>
    public delegate void ToastAction(string arg);

    /// <summary>
    /// 继承自NotificationActivator 本来是为了使用OnActivated回调
    /// 结果没有用,只能在每个Toast创建时创建回调
    /// </summary>
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(INotificationActivationCallback))]
    [Guid("7ddba60f-e2f0-4373-8098-0eafb79ba54a"), ComVisible(true)]
    public class NotificationService : NotificationActivator {
        #region Methods
        public void Init(string appid) {
            DesktopNotificationManagerCompat.RegisterAumidAndComServer<NotificationService>(appid);
            DesktopNotificationManagerCompat.RegisterActivator<NotificationService>();
        }

        /// <summary>
        /// 通知响应事件
        /// </summary>
        public event ToastAction ToastCallback;

        /// <summary>
        /// 微软提供的回调,但是目前没有响应
        /// </summary>
        public override void OnActivated(string arguments, NotificationUserInput userInput, string appUserModelId) { }

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

            ShowToast(xml);
        }

        /// <summary>
        /// 发送一条通知 （标题/文本/自定义命令）
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">文本</param>
        /// <param name="commands">自定义命令组</param>
        public void Notify(string title, string content, params ToastCommands[] commands) {
            XmlDocument xml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            XmlNodeList lines = xml.GetElementsByTagName("text");
            lines[0].AppendChild(xml.CreateTextNode(title));
            lines[1].AppendChild(xml.CreateTextNode(content));

            AddCommands(xml, commands);
            ShowToast(xml);
        }

        /// <summary>
        /// 发送一条通知 （自定义图标/标题/文本）
        /// </summary>
        /// <param name="picuri">自定义图标路径</param>
        /// <param name="title">标题</param>
        /// <param name="content">文本</param>
        public void Notify(string picuri, string title, string content) {
            XmlDocument xml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            XmlNodeList lines = xml.GetElementsByTagName("text");
            lines[0].AppendChild(xml.CreateTextNode(title));
            lines[1].AppendChild(xml.CreateTextNode(content));

            AddBigLogo(xml, picuri);
            ShowToast(xml);
        }

        /// <summary>
        /// 发送一条通知 （自定义图标/标题/文本/自定义命令）
        /// </summary>
        /// <param name="picuri">自定义图标路径</param>
        /// <param name="title">标题</param>
        /// <param name="content">文本</param>
        /// <param name="commands">自定义命令组</param>
        public void Notify(string picuri, string title, string content, params ToastCommands[] commands) {
            XmlDocument xml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            XmlNodeList lines = xml.GetElementsByTagName("text");
            lines[0].AppendChild(xml.CreateTextNode(title));
            lines[1].AppendChild(xml.CreateTextNode(content));

            AddBigLogo(xml, picuri);
            AddCommands(xml, commands);
            ShowToast(xml);
        }

        /// <summary>
        /// 发送通知
        /// </summary>
        private void ShowToast(XmlDocument xml) {
            ToastNotification toast = new ToastNotification(xml);
            toast.Activated += Toast_Activated;
            toast.Dismissed += Toast_Dismissed;
            DesktopNotificationManagerCompat.CreateToastNotifier().Show(toast);
        }

        /// <summary>
        /// 为当前通知添加交互操作
        /// </summary>
        private void AddCommands(XmlDocument xml, params ToastCommands[] commands) {
            XmlElement root = (XmlElement)xml.GetElementsByTagName("toast")[0];
            XmlElement actions = xml.CreateElement("actions");
            root.AppendChild(actions);

            foreach (var command in commands) {
                XmlElement action = xml.CreateElement("action");
                action.SetAttribute("activationType", "foreground");
                action.SetAttribute("arguments", command.Argument);
                action.SetAttribute("content", command.Content);
                actions.AppendChild(action);
            }
        }

        /// <summary>
        /// 为通知添加大标签
        /// </summary>
        private void AddBigLogo(XmlDocument xml, string logopath) {
            //获得binding组
            XmlElement binding = (XmlElement)xml.GetElementsByTagName("binding")[0];
            binding.SetAttribute("template", "ToastGeneric");
            //创建大图标
            XmlElement image = xml.CreateElement("image");
            image.SetAttribute("placement", "appLogoOverride");
            image.SetAttribute("src", logopath);
            binding.AppendChild(image);
        }

        /// <summary>
        /// 交互响应
        /// </summary>
        private void Toast_Activated(ToastNotification sender, object args) {
            var arg = ((ToastActivatedEventArgs)args);
            ToastCallback?.Invoke(arg.Arguments);
        }

        /// <summary>
        /// 消失原因
        /// </summary>
        private void Toast_Dismissed(ToastNotification sender, ToastDismissedEventArgs args) {
            //区别通知类型
            ToastCallback?.Invoke("Dismissed:" + args.Reason.ToString());
        }

        /// <summary>
        /// 清除对应App的所有通知
        /// </summary>
        /// <param name="appid">app标识</param>
        public void ClearHistory(string appid) {
            new DesktopNotificationHistoryCompat(appid).Clear();
        }
        #endregion
    }

}
