using System.Collections.Generic;

namespace GameFramework.GameException
{
    /// <summary>
    /// 框架统一的异常接口（用于统一捕获 / 日志 / 上报处理）。
    /// <para> 注：这是一个接口，不替代 System.Exception，本框架仍然使用 Exception 继承链。</para>
    /// </summary>
    public interface IGameException
    {
        /// <summary>
        /// 框架内错误码（可选，用于上报/本地处理）
        /// </summary>
        int ErrorCode { get; }

        /// <summary>
        /// 错误的严重级别
        /// </summary>
        ExceptionSeverity Severity { get; }

        /// <summary>
        /// 可选的上下文数据，便于定位问题（非序列化/仅调试用）
        /// </summary>
        IReadOnlyDictionary<string , string> Context { get; }

        /// <summary>
        /// 是否可重试（业务含义）
        /// </summary>
        bool CanRetry { get; }
    }
}
