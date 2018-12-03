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

        #endregion

        #region Methods



        #endregion

        #region Constructors
        public FlowInfoViewModel() {
                 
        }

        #endregion
    }

}
