using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ToastHelper {

    /// <summary>
    /// Toast通知的按钮命令
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ToastCommands {
        #region Properties
        public string Argument;

        public string Content;
        #endregion

        #region Constructors
        public ToastCommands(string arg, string content) {
            Argument = arg;
            Content = content;
        }
        #endregion
    }

}
