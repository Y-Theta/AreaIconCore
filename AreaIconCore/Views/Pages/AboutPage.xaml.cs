using AreaIconCore.Services;
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

namespace AreaIconCore.Views.Pages {
    /// <summary>
    /// AboutPage.xaml 的交互逻辑
    /// </summary>
    public partial class AboutPage : Page {
        public AboutPage() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (AppearanceManager.Instance.ThemeNow["ThemeDic"].ToString() == "Default")
                AppearanceManager.Instance.SwitchTheme(App.GetDirectory(Models.DirectoryKind.Theme) + "TestTheme.xaml");
            else
                AppearanceManager.Instance.SwitchTheme(null);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {

            AppearanceManager.Instance.SwitchLanguage(App.GetDirectory(Models.DirectoryKind.Lang) + "en_us.xaml");

        }
    }
}
