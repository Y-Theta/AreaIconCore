using AreaIconCore;
using AreaIconCore.Models;
using AreaIconCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WNMP_ex.Services;

namespace WNMP_ex.Controls {
    /// <summary>
    /// ServerItem.xaml 的交互逻辑
    /// </summary>
    public partial class ServerItem : UserControl {

        #region Porperties
        private Timer _seropttimer;
        private bool _test;

        #region General
        public bool IsServerRuning {
            get { return (bool)GetValue(IsServerRuningProperty); }
            set { SetValue(IsServerRuningProperty, value); }
        }
        public static readonly DependencyProperty IsServerRuningProperty =
            DependencyProperty.Register("IsServerRuning", typeof(bool),
                typeof(ServerItem), new PropertyMetadata(false, OnIsServerRuningChanged));

        private static void OnIsServerRuningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ServerItem s = (ServerItem)d;
            if ((bool)e.NewValue)
                s.Open();
            else
                s.Stop();
        }

        public string ItemPath {
            get { return (string)GetValue(ItemPathProperty); }
            set { SetValue(ItemPathProperty, value); }
        }
        public static readonly DependencyProperty ItemPathProperty =
            DependencyProperty.Register("ItemPath", typeof(string),
                typeof(ServerItem), new PropertyMetadata(""));

        public double CanvasSize {
            get { return (double)GetValue(CanvasSizeProperty); }
            set { SetValue(CanvasSizeProperty, value); }
        }
        public static readonly DependencyProperty CanvasSizeProperty =
            DependencyProperty.Register("CanvasSize", typeof(double),
                typeof(ServerItem), new PropertyMetadata(256.0));

        public double PathOffset {
            get { return (double)GetValue(PathOffsetProperty); }
            set { SetValue(PathOffsetProperty, value); }
        }
        public static readonly DependencyProperty PathOffsetProperty =
            DependencyProperty.Register("PathOffset", typeof(double),
                typeof(ServerItem), new PropertyMetadata(0.0));

        public Brush EnableColor {
            get { return (Brush)GetValue(EnableColorProperty); }
            set { SetValue(EnableColorProperty, value); }
        }
        public static readonly DependencyProperty EnableColorProperty =
            DependencyProperty.Register("EnableColor", typeof(Brush),
                typeof(ServerItem), new PropertyMetadata(null, OnColorChanged));

        public Brush DisableColor {
            get { return (Brush)GetValue(DisableColorProperty); }
            set { SetValue(DisableColorProperty, value); }
        }
        public static readonly DependencyProperty DisableColorProperty =
            DependencyProperty.Register("DisableColor", typeof(Brush),
                typeof(ServerItem), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 200, 200, 200)), OnColorChanged));

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ServerItem s = (ServerItem)d;
        }

        public Brush MaskColor {
            get { return (Brush)GetValue(MaskColorProperty); }
            set { SetValue(MaskColorProperty, value); }
        }
        public static readonly DependencyProperty MaskColorProperty =
            DependencyProperty.Register("MaskColor", typeof(Brush),
                typeof(ServerItem), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(128, 0, 0, 0))));

        public ServerStatus CommandParameter {
            get { return (ServerStatus)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(ServerStatus),
                typeof(ServerItem), new PropertyMetadata(ServerStatus.Stop));
        #endregion

        #endregion

        private void Open() {
            if (!_test) {
                App.ToastNotifier.Notify(ConstTable.AppIcon, "WNMP", $"{CommandParameter}已开启");
                ServerManager.Instance.SwitchStatus(CommandParameter);
            }
            PlayAnimation();
        }

        private void Stop() {
            if (!_test) {
                App.ToastNotifier.Notify(ConstTable.AppIcon, "WNMP", $"{CommandParameter}已关闭");
                ServerManager.Instance.SwitchStatus(CommandParameter, false);
            }
            PlayAnimation();
        }

        public void SelfTest() {
            _test = IsServerRuning != ServerManager.Instance.TestServer(CommandParameter);
            IsServerRuning = ServerManager.Instance.TestServer(CommandParameter);
        }

        private void PlayAnimation() {
            _test = false;
            server_ctl_btn.IsEnabled = false;
            server_change.Visibility = Visibility.Visible;
            _seropttimer.Start();
        }

        protected override void OnInitialized(EventArgs e) {
            SetAnimationParameters();
            base.OnInitialized(e);
        }

        private void SetAnimationParameters() {
            _seropttimer = new Timer(800);
            _seropttimer.Elapsed += _seropttimer_Elapsed;
        }

        private void _seropttimer_Elapsed(object sender, ElapsedEventArgs e) {
            this.Dispatcher.BeginInvoke(new Action(() => {
                server_change.Visibility = Visibility.Collapsed;
                server_ctl_btn.IsEnabled = true;
                if (!IsServerRuning)
                    server_icon.Fill = DisableColor;
                else
                    server_icon.Fill = EnableColor;
            }));
            _seropttimer.Stop();
        }

        public ServerItem() {
            InitializeComponent();
        }
    }
}
