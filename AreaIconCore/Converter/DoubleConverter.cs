using System;
using System.Globalization;
using System.Windows.Data;
using YFrameworkBase;

namespace AreaIconCore.Converter {

    /// <summary>
    /// DoubleConverter的操作类型
    /// </summary>
    public enum DCOperationType {
        Add,
        Min,
        Mul,
        Div
    }

    /// <summary>
    /// DoubleConverter的参数
    /// </summary>
    public class DCArgs {
        public double Num { get; set; }
        public DCOperationType OpType { get; set; }
    }

    /// <summary>
    /// 在元数据上进行一些修正操作
    /// </summary>
    public class DoubleConverter : SingletonBase<DoubleConverter>, IValueConverter {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is DCArgs arg) {
                switch (arg.OpType) {
                    case DCOperationType.Add:
                        return (double)value + arg.Num;
                    case DCOperationType.Min:
                        return (double)value - arg.Num;
                    case DCOperationType.Mul:
                        return (double)value * arg.Num;
                    case DCOperationType.Div:
                        return (double)value / arg.Num;
                }
            }
            return value;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is DCArgs arg) {
                switch (arg.OpType) {
                    case DCOperationType.Add:
                        return (double)value - arg.Num;
                    case DCOperationType.Min:
                        return (double)value + arg.Num;
                    case DCOperationType.Mul:
                        return (double)value / arg.Num;
                    case DCOperationType.Div:
                        return (double)value * arg.Num;
                }
            }
            return value;
        }
    }

}
