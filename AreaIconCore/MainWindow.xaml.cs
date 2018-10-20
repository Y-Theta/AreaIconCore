using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows;
using YControls;
using Color = System.Drawing.Color;
using Image = System.Drawing.Image;
using Pen = System.Drawing.Pen;
using Drawing = System.Drawing;
using AreaIconCore.Services;

namespace AreaIconCore {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : YT_Window {
        private readonly string _iconName = "Mainwindow";

        private IntPtr _ico = IntPtr.Zero;


        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            AllowAreaIcon = true;
            RegisterAreaIcon(_iconName);
            AreaIcons[_iconName].IconMouseDoubleClick += MainWindow_IconMouseDoubleClick;
            
        }

        private void MainWindow_IconMouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e) {
            GC.Collect(2, GCCollectionMode.Forced, true);
            if (Visibility == Visibility.Hidden)
                Show();
            else
                Hide();

            
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {

        }
    }
}
