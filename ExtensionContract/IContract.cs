using System;
using System.Collections.Generic;

namespace ExtensionContract {

    public delegate void OnExtensionAction(object sender, ApplicationScenario scenario, object para);

    /// <summary>
    /// 插件约定
    /// </summary>
    public interface IInnerDomainExtensionContract {

        /// <summary>
        /// 插件名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 插件作者
        /// </summary>
        string Author { get; set; }

        /// <summary>
        /// 插件版本
        /// </summary>
        string Edition { get; set; }

        /// <summary>
        /// 插件描述
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// 插件应用场景字典,存储应用场景及对应优先级
        /// 优先级：在相同应用场景下用以区别插件程序的执行顺序或执行与否
        /// </summary>
        Dictionary<ApplicationScenario, int> Application { get; set; }

        /// <summary>
        /// 控件数据流通知
        /// </summary>
        event OnExtensionAction Notify;

        /// <summary>
        /// 触发PostData,通知主程序接收数据
        /// </summary>
        void PostData(object sender, ApplicationScenario scenario, object para);

        /// <summary>
        /// 获取插件信息的字符描述
        /// </summary>
        string InfoStringFormat();

        /// <summary>
        /// 运行插件并带回必要值
        /// </summary>
        object Run(ApplicationScenario c, object arg = null);
    }
}