using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IPGW_ex.Controls {
    /// <summary>
    /// 带图标标签项目
    /// </summary>
    public class IconLabelItem : Control {
        #region Properties
        /// <summary>
        /// 项目符号
        /// </summary>
        public string Icon {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string),
                typeof(IconLabelItem), new PropertyMetadata(null));

        /// <summary>
        /// 另一状态下的图标
        /// </summary>
        public string ToggelIcon {
            get { return (string)GetValue(ToggelIconProperty); }
            set { SetValue(ToggelIconProperty, value); }
        }
        public static readonly DependencyProperty ToggelIconProperty =
            DependencyProperty.Register("ToggelIcon", typeof(string), 
                typeof(IconLabelItem), new PropertyMetadata(null));

        /// <summary>
        /// 图标的字体
        /// </summary>
        public FontFamily IconFamily {
            get { return (FontFamily)GetValue(IconFamilyProperty); }
            set { SetValue(IconFamilyProperty, value); }
        }
        public static readonly DependencyProperty IconFamilyProperty =
            DependencyProperty.Register("IconFamily", typeof(FontFamily),
                typeof(IconLabelItem), new PropertyMetadata(null));

        /// <summary>
        /// 图标字重
        /// </summary>
        public FontWeight IconWeight {
            get { return (FontWeight)GetValue(IconWeightProperty); }
            set { SetValue(IconWeightProperty, value); }
        }
        public static readonly DependencyProperty IconWeightProperty =
            DependencyProperty.Register("IconWeight", typeof(FontWeight),
                typeof(IconLabelItem), new PropertyMetadata(FontWeight.FromOpenTypeWeight(400)));

        /// <summary>
        /// 图标大小
        /// </summary>
        public double IconSize {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), 
                typeof(IconLabelItem), new PropertyMetadata(12.0));

        /// <summary>
        /// 图标占位宽度
        /// </summary>
        public double IconWidth {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double),
                typeof(IconLabelItem), new PropertyMetadata(double.NaN));

        /// <summary>
        /// 图标的对齐方式
        /// </summary>
        public TextAlignment IconAlignment {
            get { return (TextAlignment)GetValue(IconAlignmentProperty); }
            set { SetValue(IconAlignmentProperty, value); }
        }
        public static readonly DependencyProperty IconAlignmentProperty =
            DependencyProperty.Register("IconAlignment", typeof(TextAlignment),
                typeof(IconLabelItem), new PropertyMetadata(TextAlignment.Left));

        /// <summary>
        /// 图标可见性
        /// </summary>
        public Visibility IconVisibility {
            get { return (Visibility)GetValue(IconVisibilityProperty); }
            set { SetValue(IconVisibilityProperty, value); }
        }
        public static readonly DependencyProperty IconVisibilityProperty =
            DependencyProperty.Register("IconVisibility", typeof(Visibility), 
                typeof(IconLabelItem), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName {
            get { return (string)GetValue(ItemNameProperty); }
            set { SetValue(ItemNameProperty, value); }
        }
        public static readonly DependencyProperty ItemNameProperty =
            DependencyProperty.Register("ItemName", typeof(string), 
                typeof(IconLabelItem), new PropertyMetadata(null));

        /// <summary>
        /// 项目名称占位宽度
        /// </summary>
        public double ItemNameWidth {
            get { return (double)GetValue(ItemNameWidthProperty); }
            set { SetValue(ItemNameWidthProperty, value); }
        }
        public static readonly DependencyProperty ItemNameWidthProperty =
            DependencyProperty.Register("ItemNameWidth", typeof(double),
                typeof(IconLabelItem), new PropertyMetadata(Double.NaN));

        /// <summary>
        /// 项目名称可见性
        /// </summary>
        public Visibility ItemNameVisibility {
            get { return (Visibility)GetValue(ItemNameVisibilityProperty); }
            set { SetValue(ItemNameVisibilityProperty, value); }
        }
        public static readonly DependencyProperty ItemNameVisibilityProperty =
            DependencyProperty.Register("ItemNameVisibility", typeof(Visibility), 
                typeof(IconLabelItem), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// 项目名称字体对齐方式
        /// </summary>
        public TextAlignment ItemNameTextAlignment {
            get { return (TextAlignment)GetValue(ItemNameTextAlignmentProperty); }
            set { SetValue(ItemNameTextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty ItemNameTextAlignmentProperty =
            DependencyProperty.Register("ItemNameTextAlignment", typeof(TextAlignment),
                typeof(IconLabelItem), new PropertyMetadata(TextAlignment.Left));

        /// <summary>
        /// 项目值
        /// </summary>
        public string ItemValue {
            get { return (string)GetValue(ItemValueProperty); }
            set { SetValue(ItemValueProperty, value); }
        }
        public static readonly DependencyProperty ItemValueProperty =
            DependencyProperty.Register("ItemValue", typeof(string),
                typeof(IconLabelItem), new PropertyMetadata(null));

        /// <summary>
        /// 项目值字体对齐方式
        /// </summary>
        public TextAlignment ItemValueTextAlignment {
            get { return (TextAlignment)GetValue(ItemValueTextAlignmentProperty); }
            set { SetValue(ItemValueTextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty ItemValueTextAlignmentProperty =
            DependencyProperty.Register("ItemValueTextAlignment", typeof(TextAlignment),
                typeof(IconLabelItem), new PropertyMetadata(TextAlignment.Left));

        /// <summary>
        /// 项目值可见性
        /// </summary>
        public Visibility ItemValueVisibility {
            get { return (Visibility)GetValue(ItemValueVisibilityProperty); }
            set { SetValue(ItemValueVisibilityProperty, value); }
        }
        public static readonly DependencyProperty ItemValueVisibilityProperty =
            DependencyProperty.Register("ItemValueVisibility", typeof(Visibility),
                typeof(IconLabelItem), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// 鼠标提示字段
        /// </summary>
        public string ToolTipString {
            get { return (string)GetValue(ToolTipStringProperty); }
            set { SetValue(ToolTipStringProperty, value); }
        }
        public static readonly DependencyProperty ToolTipStringProperty =
            DependencyProperty.Register("ToolTipString", typeof(string), 
                typeof(IconLabelItem), new PropertyMetadata(null));

        /// <summary>
        /// 是否开启第二模式
        /// </summary>
        public bool ToggelMode {
            get { return (bool)GetValue(ToggelModeProperty); }
            set { SetValue(ToggelModeProperty, value); }
        }
        public static readonly DependencyProperty ToggelModeProperty =
            DependencyProperty.Register("ToggelMode", typeof(bool), 
                typeof(IconLabelItem), new PropertyMetadata(false));
        #endregion

        #region Methods
        #endregion

        #region Constructors
        static IconLabelItem() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconLabelItem), new FrameworkPropertyMetadata(typeof(IconLabelItem)));
        }
        #endregion
    }

}
