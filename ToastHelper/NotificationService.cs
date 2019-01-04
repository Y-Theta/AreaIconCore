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
    /// <param name="app">app的id</param>
    /// <param name="arg">按钮的参数</param>
    /// <param name="kvs">用户输入</param>
    public delegate void ToastAction(string app, string arg, List<KeyValuePair<string, string>> kvs);

    /// <summary>
    /// 继承自NotificationActivator 本来是为了使用OnActivated回调
    /// 结果没有用,只能在每个Toast创建时创建回调
    /// 
    /// 
    /// 最好用其他类继承本类，并将下面的特性拷贝到新类上，换上自己的GUID，这是为了测试方便
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
        /// 通知响应事件,在使用时接收
        /// </summary>
        public event ToastAction ToastCallback;

        /// <summary>
        /// 微软提供的回调,但是目前没有响应
        /// </summary>
        public override void OnActivated(string arguments, NotificationUserInput userInput, string appUserModelId) {
            //foreach (var key in userInput.Keys) {
            //    Console.WriteLine(key);
            //    Console.WriteLine(userInput[key]);
            //}
            //Console.WriteLine(appUserModelId);
            List<KeyValuePair<string, string>> kvs = new List<KeyValuePair<string, string>>();
            if (userInput != null && userInput.Count > 0)
                foreach (var key in userInput.Keys) {
                    kvs.Add(new KeyValuePair<string, string>(key, userInput[key]));
                }
            ToastCallback?.Invoke(appUserModelId, arguments, kvs);
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
        /// ————————————————————————————————————————————————————————
        /// 测试Input类型使用
        /// </summary>
        public void Notify(string title, string content, bool flag) {
            XmlDocument xml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            //获得binding组
            XmlElement binding = (XmlElement)xml.GetElementsByTagName("binding")[0];
            binding.SetAttribute("template", "ToastGeneric");

            XmlNodeList lines = xml.GetElementsByTagName("text");
            lines[0].AppendChild(xml.CreateTextNode(title));
            lines[1].AppendChild(xml.CreateTextNode(content));

            XmlElement root = (XmlElement)xml.GetElementsByTagName("toast")[0];
            XmlElement actions = xml.CreateElement("actions");

            XmlElement input = xml.CreateElement("input");
            input.SetAttribute("type", "text");
            input.SetAttribute("id", "textBox");
            input.SetAttribute("placeHolderContent", "reply");
            actions.AppendChild(input);
            root.AppendChild(actions);

            ShowToast(xml);
        }

        /// <summary>
        /// 发送通知
        /// </summary>
        private void ShowToast(XmlDocument xml) {
            ToastNotification toast = new ToastNotification(xml);
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
        /// 添加输入框
        /// </summary>
        private void AddInput(XmlDocument xml) {
            XmlElement actions = (XmlElement)xml.GetElementsByTagName("actions")[0];

            XmlElement input = xml.CreateElement("input");
            input.SetAttribute("type", "text");
            input.SetAttribute("id", "textBox");
            input.SetAttribute("placeHolderContent", "test");
            actions.AppendChild(input);
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
        /// 清除对应App的所有通知
        /// </summary>
        /// <param name="appid">app标识</param>
        public void ClearHistory(string appid) {
            new DesktopNotificationHistoryCompat(appid).Clear();
        }
        #endregion
    }

}
