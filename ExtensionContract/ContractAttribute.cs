using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionContract {
    /// <summary>
    /// 导出标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ExportContractAttribute : Attribute {
        public Type ExpType { get; set; }

        public ExportContractAttribute(Type type) {
            ExpType = type;
        }
    }

    /// <summary>
    /// 导入标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ImportContractAttribute : Attribute {
        public Type ImpType { get; set; }

        public ImportContractAttribute(Type type) {
            ImpType = type;
        }
    }
}
