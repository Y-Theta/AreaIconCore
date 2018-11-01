using D6_ex.ViewModels;
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

namespace D6_ex {
    /// <summary>
    /// FluxPage.xaml 的交互逻辑
    /// </summary>
    public partial class FluxPage : Page {
        private FluxPageViewModel _fpvm;

        public FluxPage() {
            InitializeComponent();
            Loaded += FluxPage_Loaded;
        }

        private void FluxPage_Loaded(object sender, RoutedEventArgs e) {
            _fpvm = (FluxPageViewModel)this.DataContext;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            this.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
        }
    }
}
