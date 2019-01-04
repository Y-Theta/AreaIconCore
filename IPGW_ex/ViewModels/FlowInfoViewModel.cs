using IPGW_ex.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;
using YFrameworkBase;

namespace IPGW_ex.ViewModels {
    /// <summary>
    /// 因为只实现单一功能，所以将功能富集到一个类上
    /// </summary>
    internal class FlowInfoViewModel : ViewModelBase<FlowInfoViewModel> {
        #region Properties

        /// <summary>
        /// 当前网络连接状态
        /// </summary>
        private bool _connectstate;
        public bool ConnectState {
            get => _connectstate;
            set => SetValue(out _connectstate, value, ConnectState);
        }

        /// <summary>
        /// 当前登录的用户
        /// </summary>
        private string _presentAccount;
        public string PresentAccount {
            get => _presentAccount;
            set => SetValue(out _presentAccount, value, PresentAccount);
        }

        /// <summary>
        /// 当前用户的密码
        /// </summary>
        private string _presentPassword;
        public string PresentPassword {
            get => _presentPassword;
            set => SetValue(out _presentPassword, value, PresentPassword);
        }

        #endregion

        #region Methods



        #endregion

        #region Constructors
        public FlowInfoViewModel() {
                 
        }

        #endregion
    }

}
