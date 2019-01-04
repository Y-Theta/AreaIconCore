using AreaIconCore.Models;
using AreaIconCore.Services;
using AreaIconCore.ViewModels;
using ExtensionContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using YControls.Command;
using YFrameworkBase.Setting;

namespace AreaIconCore.Controls {
    /// <summary>
    /// 插件项控件样式
    /// </summary>
    public class ExtensionItem : SettingItem {
        #region Properties
        /// <summary>
        /// 与之关联的控件
        /// </summary>
        public ExtensionInfo ExtensionKey {
            get { return (ExtensionInfo)GetValue(ExtensionKeyProperty); }
            set { SetValue(ExtensionKeyProperty, value); }
        }
        public static readonly DependencyProperty ExtensionKeyProperty =
            DependencyProperty.Register("ExtensionKey", typeof(ExtensionInfo),
                typeof(ExtensionItem), new PropertyMetadata(null, OnExtensionKeyChanged));
        private static void OnExtensionKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ExtensionInfo newinfo = (ExtensionInfo)e.NewValue;
            ((ExtensionItem)d).ExtensionEnable = CoreSettings.Instance.Extensions[newinfo.AssemblyName].Enabled;
        }

        /// <summary>
        /// 控件说明文本样式
        /// </summary>
        public Style ExtensionDescriptionStyle {
            get { return (Style)GetValue(ExtensionDescriptionStyleProperty); }
            set { SetValue(ExtensionDescriptionStyleProperty, value); }
        }
        public static readonly DependencyProperty ExtensionDescriptionStyleProperty =
            DependencyProperty.Register("ExtensionDescriptionStyle", typeof(Style), 
                typeof(ExtensionItem), new PropertyMetadata(null));

        /// <summary>
        /// 控件状态
        /// </summary>
        public bool ExtensionEnable {
            get { return (bool)GetValue(ExtensionEnableProperty); }
            set { SetValue(ExtensionEnableProperty, value); }
        }
        public static readonly DependencyProperty ExtensionEnableProperty =
            DependencyProperty.Register("ExtensionEnable", typeof(bool),
                typeof(ExtensionItem), new PropertyMetadata(false, OnExtensionEnableChanged));
        private static void OnExtensionEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ExtensionInfo newinfo = ((ExtensionItem)d).ExtensionKey;
            CoreSettings.Instance.Extensions[newinfo.AssemblyName].Enabled = (bool)e.NewValue;
            SettingManager.Instance.SaveSetting<CoreSettings>();
        }

        /// <summary>
        /// 按钮命令
        /// </summary>
        public CommandBase Operation { get; set; }
        #endregion

        #region Methods
        
        private void InitCommands() {
            Operation = new CommandBase();
            Operation.Execution += Operation_Execution;
        }

        protected override void OnInitialized(EventArgs e) {
            InitCommands();
            base.OnInitialized(e);
        }

        /// <summary>
        /// 对相关插件的操作
        /// </summary>
        private void Operation_Execution(object para = null) {
            switch (para) {
                case "SETTING":
                    if (HostAdapter.Instance.ExtensionDirectory.ContainsKey(ExtensionKey.Name))
                        MainWindowViewModel.Singleton.NavigateTo(HostAdapter.Instance.ExtensionDirectory[ExtensionKey.Name].Run(ApplicationScenario.SettingPage));
                    break;
                default:break;
            }
        }
        #endregion

        #region Constructors
        static ExtensionItem() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtensionItem), new FrameworkPropertyMetadata(typeof(ExtensionItem)));
        }
        #endregion
    }

}
