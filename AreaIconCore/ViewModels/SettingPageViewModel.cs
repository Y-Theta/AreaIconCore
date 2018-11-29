using AreaIconCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using YControls.Command;
using YControls.FlowControls;
using YFrameworkBase;
using YFrameworkBase.Setting;

namespace AreaIconCore.ViewModels {
    public class SettingPageViewModel : ViewModelBase<SettingPageViewModel> {
        #region Properties

        public CommandBase SettingPageCommands { get; set; }
        #endregion

        #region Methods
        private void InitCommands() {
            SettingPageCommands = new CommandBase();
            SettingPageCommands.Execution += SettingPageCommands_Execution;
        }

        private void SettingPageCommands_Execution(object para = null) {
            switch (para) {
                case "Save":
                    try {
                        SettingManager.Instance.SaveSetting<CoreSettings>();
                        PopupMessageManager.Instance.Message(App.FindRes<string>("SettingPage_Save_OKMessage"));
                    }
                    catch {
                        PopupMessageManager.Instance.Message(App.FindRes<string>("SettingPage_Save_FailedMessage"));
                    }
                    break;
                case "Clear":
                    break;
            }
        }
        #endregion

        #region Constructors
        public SettingPageViewModel() {
            InitCommands();
        }
        #endregion
    }

}
