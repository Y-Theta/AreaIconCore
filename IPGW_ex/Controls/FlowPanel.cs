using IPGW_ex.Model;
using IPGW_ex.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YControls.Command;

namespace IPGW_ex.Controls {
    /// <summary>
    /// 流量面板 将会显示在弹出面板
    /// </summary>
    public class FlowPanel : Control {
        #region Properties
        /// <summary>
        /// 流量绑定
        /// </summary>
        Binding _flowBinding;

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
        /// 按钮命令
        /// </summary>
        public CommandBase Action { get; set; }
        #endregion

        #region Methods
        public override void OnApplyTemplate() {
            Action = new CommandBase();
            Action.Execution += Action_Execution;
            _flowBinding = new Binding {
                Source = IpgwSetting.Instance,
                Path = new PropertyPath("LatestFlow"),
                Mode = BindingMode.OneWay,
            };
            SetBinding(FLOWProperty, _flowBinding);
            base.OnApplyTemplate();

            IsVisibleChanged += FlowPanel_IsVisibleChanged;
        }

        /// <summary>
        /// 按钮操作
        /// </summary>
        private void Action_Execution(object para = null) {
            switch (para) {
                case "Update":

                    break;
                case "DisConnect":

                    break;
                default:break;
            }
        }

        private void FlowPanel_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if ((bool)e.NewValue) {
                FLOW = null;
                SetBinding(FLOWProperty, _flowBinding);
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
