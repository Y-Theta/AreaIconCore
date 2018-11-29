using IPGW_ex.Model;
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

namespace IPGW_ex.Controls {
    public class FlowPanel : Control {

        public FlowInfo FLOW {
            get { return (FlowInfo)GetValue(FLOWProperty); }
            set { SetValue(FLOWProperty, value); }
        }
        public static readonly DependencyProperty FLOWProperty =
            DependencyProperty.Register("FLOW", typeof(FlowInfo), 
                typeof(FlowPanel), new PropertyMetadata(null));

        static FlowPanel() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlowPanel), new FrameworkPropertyMetadata(typeof(FlowPanel)));
        }
    }
}
