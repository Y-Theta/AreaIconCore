using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using YControls.FlowControls;
using YFrameworkBase;

namespace AreaIconCore.Services {
    /// <summary>
    /// 用于在程序中弹出通知浮窗
    /// </summary>
    public class PopupMessageManager : SingletonBase<PopupMessageManager> {
        #region Properties
        /// <summary>
        /// 消息Popup控件
        /// 采用即时消息
        /// </summary>
        private YT_PopupBase _popholder;
        #endregion

        #region Methods
        /// <summary>
        /// 发出一条文本通知
        /// </summary>
        public void Message(string content) {
            if (_popholder.IsOpen)
                _popholder.IsOpen = false;
            _popholder.Style = App.FindRes<Style>("MessageTextPopup");
            ContentControl contentc = new ContentControl {
                Style = App.FindRes<Style>("MessagePopupContent"),
                MaxWidth = 256,
            };
            TextBlock text = new TextBlock {
                Style = App.FindRes<Style>("MessageStringFormat"),
                Text = content,
                TextWrapping = TextWrapping.Wrap,
            };
            contentc.Content = text;
            _popholder.Child = contentc;
            _popholder.IsOpen = true;
        }

        /// <summary>
        /// 发出一条带有内容的通知
        /// </summary>
        public void Message(UIElement content) {
            if (_popholder.IsOpen)
                _popholder.IsOpen = false;
            _popholder.Style = App.FindRes<Style>("MessageTextPopup") as Style;
            _popholder.Child = content;
            _popholder.IsOpen = true;
        }

        protected void Init() {
            _popholder = new YT_PopupBase {
                PlacementTarget = App.Current.MainWindow,
                Placement = System.Windows.Controls.Primitives.PlacementMode.RelativePoint
            };
        }
        #endregion

        #region Constructors
        public PopupMessageManager() {
            Init();
        }
        #endregion
    }

}
