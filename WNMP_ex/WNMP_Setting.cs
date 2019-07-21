///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WNMP_ex.Services;
using YFrameworkBase.Setting;

namespace WNMP_ex {
    [SettingFile("settings", "wnmp_setting.xml")]
    [DataContract]
    public class WNMP_Setting : ALocalSetting<WNMP_Setting> {
        #region Properties
        /// <summary>
        /// 默认开启服务器的方式
        /// </summary>
        [DataMember]
        private ServerStatus _defaultstatus;
        public ServerStatus Defaultstatus {
            get => _defaultstatus;
            set => SetValue(out _defaultstatus, value, Defaultstatus);
        }

        /// <summary>
        /// 路径字典
        /// </summary>
        [DataMember]
        private Dictionary<string, string> _pathmap;
        public Dictionary<string, string> PathMap {
            get => _pathmap;
            set => SetValue(out _pathmap, value, PathMap);
        }
        #endregion

        #region Methods
        public void Init() {
            PathMap = new Dictionary<string, string> {
               {Properties.Resources.PHP_ROOT ,@"C:\WNMP\PHP\php-7.2.16-nts\"},
               {Properties.Resources.PHP_PORT ,"127.0.0.1:9000"},
               {Properties.Resources.NGINX_ROOT ,@"C:\WNMP\Nginx\nginx-1.15.10\"},
               {Properties.Resources.MYSQL_SERVER_NAME ,"MySQL_Y"},
            };
            Defaultstatus = ServerStatus.NP;
        }


        #endregion

        #region Constructors

        public WNMP_Setting() { Init(); }
        #endregion
    }


}
