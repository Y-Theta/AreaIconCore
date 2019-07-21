using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionContract {

    /// <summary>
    /// 插件的作用场景
    /// 指示插件的Run方法在何时会被宿主程序调用，或插件具有何种权限
    /// </summary>
    public enum ApplicationScenario {
        /// <summary>
        /// 在插件被卸载时
        /// </summary>
        UnLoad,

        /// <summary>
        /// 在宿主程序加载时
        /// </summary>
        AppInit,

        /// <summary>
        /// 在宿主程序结束时
        /// </summary>
        AppEnd,

        #region Window
        /// <summary>
        /// 在宿主程序主窗口创建时
        /// </summary>
        MainWindowInit,

        /// <summary>
        /// 在宿主程序主窗口结束时
        /// </summary>
        MainWindowEnd,

        /// <summary>
        /// 作为主程序的主页面显示
        /// </summary>
        MainPage,

        /// <summary>
        /// 当前窗体是否开启透明
        /// </summary>
        BlurState,
        #region AreaIcon

        /// <summary>
        /// 在宿主程序中申请托盘图标
        /// </summary>
        AreaIcon,

        /// <summary>
        /// 托盘图标右键菜单项
        /// </summary>
        AreaContextMenu,

        /// <summary>
        /// 托盘图标的弹出框
        /// </summary>
        AreaPopup,

        #endregion

        #region Setting
        /// <summary>
        /// 当前程序拥有设置页
        /// </summary>
        SettingPage,


        #endregion

        /// <summary>
        /// 在程序退出时执行
        /// </summary>
        SessionEnding,
        #endregion
    }
}
