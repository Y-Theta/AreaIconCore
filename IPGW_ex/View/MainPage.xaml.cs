using IPGW_ex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YFrameworkBase.DataAccessor;

namespace IPGW_ex.View {
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page {
        XmlDataProvider provider = new XmlDataProvider();

        public MainPage() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            provider.AddNode(new FluxInfo { Time = DateTime.Today, Data = 20, Balance = 20 });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            Console.WriteLine(
            provider.GetNode<FluxInfo>(DateTime.Today.ToFileTime().ToString()).ToString());

        }
    }
}
