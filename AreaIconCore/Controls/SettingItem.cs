using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace AreaIconCore.Controls {
    /// <summary>
    /// 设置项容器
    /// </summary>
    [ContentProperty(nameof(Content))]
    public class SettingItem : Control {
        #region Properties
        /// <summary>
        /// 设置项键值
        /// </summary>
        public string SettingKey {
            get { return (string)GetValue(SettingKeyProperty); }
            set { SetValue(SettingKeyProperty, value); }
        }
        public static readonly DependencyProperty SettingKeyProperty =
            DependencyProperty.Register("SettingKey", typeof(string),
                typeof(SettingItem), new FrameworkPropertyMetadata(""));

        /// <summary>
        /// 设置项标签的样式
        /// </summary>
        public Style SettingKeyTemplate {
            get { return (Style)GetValue(SettingKeyTemplateProperty); }
            set { SetValue(SettingKeyTemplateProperty, value); }
        }
        public static readonly DependencyProperty SettingKeyTemplateProperty =
            DependencyProperty.Register("SettingKeyTemplate", typeof(Style),
                typeof(SettingItem), new FrameworkPropertyMetadata(null, 
                    FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 设置项控件区域
        /// </summary>
        public object Content {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object),
                typeof(SettingItem), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// 指示标在一般状态下的颜色
        /// </summary>
        public Brush IDnormal {
            get { return (Brush)GetValue(IDnormalProperty); }
            set { SetValue(IDnormalProperty, value); }
        }
        public static readonly DependencyProperty IDnormalProperty =
            DependencyProperty.Register("IDnormal", typeof(Brush), 
                typeof(SettingItem), new FrameworkPropertyMetadata(null, 
                    FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 指示标激活时的颜色
        /// </summary>
        public Brush IDactived {
            get { return (Brush)GetValue(IDactivedProperty); }
            set { SetValue(IDactivedProperty, value); }
        }
        public static readonly DependencyProperty IDactivedProperty =
            DependencyProperty.Register("IDactived", typeof(Brush),
                typeof(SettingItem), new FrameworkPropertyMetadata(null, 
                    FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 指示标的宽度
        /// </summary>
        public double IDthickness {
            get { return (double)GetValue(IDthicknessProperty); }
            set { SetValue(IDthicknessProperty, value); }
        }
        public static readonly DependencyProperty IDthicknessProperty =
            DependencyProperty.Register("IDthickness", typeof(double),
                typeof(SettingItem), new FrameworkPropertyMetadata(2.0, 
                    FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 指示标的间隙
        /// </summary>
        public Thickness IDmargin {
            get { return (Thickness)GetValue(IDmarginProperty); }
            set { SetValue(IDmarginProperty, value); }
        }
        public static readonly DependencyProperty IDmarginProperty =
            DependencyProperty.Register("IDmargin", typeof(Thickness), 
                typeof(SettingItem), new FrameworkPropertyMetadata(new Thickness(0), 
                    FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region Constructors
        static SettingItem() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SettingItem), new FrameworkPropertyMetadata(typeof(SettingItem)));
        }
        #endregion
    }

}
