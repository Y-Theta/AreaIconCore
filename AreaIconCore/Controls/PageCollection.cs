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

namespace AreaIconCore.Controls {
    /// <summary>
    /// 多页显示控件
    /// 可设置多个页面，可以在所有页面间顺序导航
    /// </summary>
    public class PageCollection : ContentControl {

        #region Properties
        /// <summary>
        /// 持有页面数
        /// </summary>
        public List<Page> AllPages {
            get { return (List<Page>)GetValue(AllPagesProperty); }
            set { SetValue(AllPagesProperty, value); }
        }
        public static readonly DependencyProperty AllPagesProperty =
            DependencyProperty.Register("AllPages", typeof(List<Page>),
                typeof(PageCollection), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits,OnPageCollectionChanged));
        private static void OnPageCollectionChanged (DependencyObject d ,DependencyPropertyChangedEventArgs e) {

        }
        #endregion

        #region Constructor
        static PageCollection() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PageCollection), new FrameworkPropertyMetadata(typeof(PageCollection)));
        }
        #endregion
    }
}
