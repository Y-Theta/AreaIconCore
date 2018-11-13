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
    public class MainPageViewModel : ViewModelBase {
        /// <summary>
        /// 单例
        /// </summary>
        #region Properties
        private static Lazy<MainPageViewModel> _singleton = new Lazy<MainPageViewModel>(()=> { return new MainPageViewModel(); });
        public static MainPageViewModel Singleton {
            get => _singleton.Value;
        }

        public CommandBase MainPageCommands { get; set; }

        public event CommandActionEventHandler MainPageActions;
        #endregion

        #region Methods

        private void InitCommand() {
            MainPageCommands = new CommandBase(obj => {
                MainPageActions?.Invoke(this, new CommandArgs(obj));
            });
        }
        #endregion

        #region Constructors
        public MainPageViewModel() {
            InitCommand();
        }
        #endregion
    }

}
