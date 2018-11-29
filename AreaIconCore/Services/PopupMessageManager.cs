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
        private YT_PopupBase _popholder { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// 发出一条文本通知
        /// </summary>
        [STAThread]
        public void Message(string content) {
            App.Current.Dispatcher.Invoke(() => {
                _popholder.Style = App.FindRes<Style>("MessageTextPopup");
                Border grid = new Border {
                    Height = 32,
                    Width = 128,
                    Background = App.FindRes<SolidColorBrush>("DefaultMessagePopup_Bg"),
                };
                TextBlock text = new TextBlock {
                    Style = App.FindRes<Style>("MessageStringFormat"),
                    Text = content
                };
                grid.Child = text;
                _popholder.Child = grid;
                _popholder.IsOpen = true;
            }, System.Windows.Threading.DispatcherPriority.Normal);
        }

        /// <summary>
        /// 发出一条带有内容的通知
        /// </summary>
        [STAThread]
        public void Message(UIElement content) {
            App.Current.Dispatcher.Invoke(() => {
                _popholder.Style = App.FindRes<Style>("MessageTextPopup") as Style;
                _popholder.Child = content;
                _popholder.IsOpen = true;
            }, System.Windows.Threading.DispatcherPriority.Normal);
        }

        protected void Init() {
            _popholder = new YT_PopupBase {
                PlacementTarget = App.Current.MainWindow
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
