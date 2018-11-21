using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YControls.Command;
using YFrameworkBase;

namespace AreaIconCore.ViewModels {
    /// <summary>
    /// 主页面ViewModel
    /// </summary>
    public class MainWindowViewModel : ViewModelBase {
        /// <summary>
        /// 单例
        /// </summary>
        #region Properties
        private static Lazy<MainWindowViewModel> _singleton = new Lazy<MainWindowViewModel>(()=> { return new MainWindowViewModel(); });
        public static MainWindowViewModel Singleton {
            get => _singleton.Value;
        }

        #region 
        /// <summary>
        /// 当前页面的图标
        /// </summary>
        private string _contentIconNow;
        public string ContentIconNow {
            get => _contentIconNow;
            set => SetValue(out _contentIconNow, value, ContentIconNow);
        }

        /// <summary>
        /// 当前页面的内容链接
        /// </summary>
        private string _contentPageNow;
        public string ContentPageNow {
            get => _contentPageNow;
            set => SetValue(out _contentPageNow, value, ContentPageNow);
        }
        #endregion

        public CommandBase MainPageCommands { get; set; }
        public CommandBase NavigateCommands { get; set; }

        public event CommandActionEventHandler MainPageActions;
        #endregion

        #region Methods
        public void UpdateContentInfo(string icon,string name) {
            ContentIconNow = icon;
            ContentPageNow = name;
        }

        private void InitCommand() {
            MainPageCommands = new CommandBase(obj => { MainPageActions?.Invoke(this, new CommandArgs(obj, "MainPage")); });
            NavigateCommands = new CommandBase(obj => { MainPageActions?.Invoke(this, new CommandArgs(obj, "Navigate")); });
        }
        #endregion

        #region Constructors
        public MainWindowViewModel() {
            InitCommand();
        }
        #endregion
    }

}
