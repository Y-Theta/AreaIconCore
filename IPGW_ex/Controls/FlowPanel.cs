using AreaIconCore;
using IPGW_ex.Model;
using IPGW_ex.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YControls.Command;
using YFrameworkBase;

namespace IPGW_ex.Controls {
    /// <summary>
    /// 流量面板 将会显示在弹出面板
    /// </summary>
    [TemplatePart(Name = "BTN_Update")]
    [TemplatePart(Name = "BTN_Close")]
    [TemplatePart(Name = "BTN_DisConnect")]
    public class FlowPanel : Control {
        #region Properties
        /// <summary>
        /// 流量绑定
        /// </summary>
        Binding _flowBinding;

        /// <summary>
        /// 由于要对Update使用动画,因此需从模板中得到实例
        /// </summary>
        Button _updatebutton;

        /// <summary>
        /// 流量信息
        /// </summary>
        internal FlowInfo FLOW {
            get { return (FlowInfo)GetValue(FLOWProperty); }
            set { SetValue(FLOWProperty, value); }
        }
        public static readonly DependencyProperty FLOWProperty =
            DependencyProperty.Register("FLOW", typeof(FlowInfo),
                typeof(FlowPanel), new PropertyMetadata(null));

        /// <summary>
        /// 网络连接状态
        /// </summary>
        public bool ConnectState {
            get { return (bool)GetValue(ConnectStateProperty); }
            set { SetValue(ConnectStateProperty, value); }
        }
        public static readonly DependencyProperty ConnectStateProperty =
            DependencyProperty.Register("ConnectState", typeof(bool),
                typeof(FlowPanel), new PropertyMetadata(false));

        /// <summary>
        /// 按钮命令
        /// </summary>
        public CommandBase Action { get; set; }
        #endregion

        #region Methods
        public override void OnApplyTemplate() {
            _updatebutton = GetTemplateChild("BTN_Update") as Button;

            base.OnApplyTemplate();
        }

        protected override void OnInitialized(EventArgs e) {
            base.OnInitialized(e);
            _flowBinding = new Binding {
                Source = IpgwSetting.Instance,
                Path = new PropertyPath("LatestFlow"),
                Mode = BindingMode.OneWay,
            };
            SetBinding(FLOWProperty, _flowBinding);
            IsVisibleChanged += FlowPanel_IsVisibleChanged;
            Action = new CommandBase();
            Action.Execution += Action_Execution;
        }

        protected override void OnMouseEnter(MouseEventArgs e) {
            base.OnMouseMove(e);
        }

        /// <summary>
        /// 按钮操作
        /// </summary>
        private async void Action_Execution(object para = null) {
            switch (para) {
                case "Update":
                    _updatebutton.IsEnabled = false;
                    await IpgwLoginService.Instance.Update(() => {
                        _updatebutton.IsEnabled = true;
                        IPGWCore.Instence.UpdateAreaIcon(FormatService.Instance.LatestPercent);
                    });
                    break;
                case "DisConnect":
                    IpgwLoginService.Instance.Logout();
                    break;
                default: break;
            }
        }

        private async void FlowPanel_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if ((bool)e.NewValue) {
                FLOW = null;
                SetBinding(FLOWProperty, _flowBinding);
                await IpgwLoginService.Instance.TestOnly();
            }
            else {
                if(App.Current != null && !App.Current.MainWindow.IsVisible)
                    WinAPI.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle.ToInt32(), -1, -1);
            }
        }

        #endregion

        #region Constructor
        static FlowPanel() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlowPanel), new FrameworkPropertyMetadata(typeof(FlowPanel)));
        }
        #endregion
    }
}
