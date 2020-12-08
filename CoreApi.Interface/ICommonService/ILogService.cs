using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApi.IService.ICommonService
{
    public interface ILogService
    {
        /// <summary>
        ///     记录致命错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogCritical(string message, params object[] args);

        /// <summary>
        ///     记录致命错误
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogCritical(Exception exception, string message, params object[] args);

        /// <summary>
        ///     记录调试信息
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogDebug(Exception exception, string message, params object[] args);

        /// <summary>
        ///     记录调试信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogDebug(string message, params object[] args);

        /// <summary>
        ///     记录错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogError(string message, params object[] args);

        /// <summary>
        ///     记录错误
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        void LogError(Exception exception, params object[] args);

        /// <summary>
        ///     记录信息
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogInformation(Exception exception, string message, params object[] args);

        /// <summary>
        ///     记录信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogInformation(string message, params object[] args);

        /// <summary>
        ///     记录跟踪信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="showMessage"></param>
        /// <param name="args"></param>
        void LogTrace(string message, bool showMessage = false, params object[] args);


        /// <summary>
        ///     记录跟踪信息
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogTrace(Exception exception, string message, params object[] args);

        /// <summary>
        ///     记录警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogWarning(string message, params object[] args);

        /// <summary>
        ///     记录警告
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogWarning(Exception exception, string message, params object[] args);

        /// <summary>
        ///     记录调试信息
        /// </summary>
        /// <param name="message"></param>
        void LogDebug(string message);

        /// <summary>
        ///     记录信息
        /// </summary>
        /// <param name="message"></param>
        void LogInformation(string message);
    }
}
