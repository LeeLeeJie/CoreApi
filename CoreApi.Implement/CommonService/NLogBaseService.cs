using System;
using System.Threading.Tasks;
using CoreApi.IService.ICommonService;
using Microsoft.Extensions.Logging;

namespace CoreApi.Service.CommonService
{
    public abstract class NLogBaseService : ILogService
    {
        //public abstract bool DebugMode { get; }

        protected abstract ILogger CurrentLogger { get; }

        /// <summary>
        ///     记录致命错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogCritical(string message, params object[] args)
        {
            Task.Run(() => { CurrentLogger?.LogCritical(message, args); });
        }

        /// <summary>
        ///     记录致命错误
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogCritical(Exception exception, string message, params object[] args)
        {
            Task.Run(() => { CurrentLogger?.LogCritical(exception, message, args); });
        }

        /// <summary>
        ///     记录调试信息
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogDebug(Exception exception, string message, params object[] args)
        {
            Task.Run(() => { CurrentLogger?.LogDebug(exception, message, args); });
        }

        /// <summary>
        ///     记录调试信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogDebug(string message, params object[] args)
        {
            Task.Run(() => { CurrentLogger?.LogDebug(message, args); });
        }

        /// <summary>
        ///     记录错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogError(string message, params object[] args)
        {
            Task.Run(() => { CurrentLogger?.LogError(message, args); });
        }

        /// <summary>
        ///     记录错误
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public void LogError(Exception exception, params object[] args)
        {
            Task.Run(() =>
            {
                CurrentLogger?.LogError(exception, $"{exception.Message},{exception.InnerException}", args);
            });
        }

        /// <summary>
        ///     记录信息
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogInformation(Exception exception, string message, params object[] args)
        {
            Task.Run(() => { CurrentLogger?.LogInformation(exception, message, args); });
        }

        /// <summary>
        ///     记录信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogInformation(string message, params object[] args)
        {
            Task.Run(() =>
            {
                CurrentLogger?.LogInformation(message, args);
            });
        }

        /// <summary>
        ///     记录跟踪信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="showMessage"></param>
        /// <param name="args"></param>
        public void LogTrace(string message, bool showMessage = false, params object[] args)
        {
            Task.Run(() => { CurrentLogger?.LogTrace(message, args); });
        }


        /// <summary>
        ///     记录跟踪信息
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogTrace(Exception exception, string message, params object[] args)
        {
            Task.Run(() => { CurrentLogger?.LogTrace(exception, message, args); });
        }

        /// <summary>
        ///     记录警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogWarning(string message, params object[] args)
        {
            Task.Run(() => { CurrentLogger?.LogWarning(message, args); });
        }

        /// <summary>
        ///     记录警告
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogWarning(Exception exception, string message, params object[] args)
        {
            Task.Run(() => { CurrentLogger?.LogWarning(exception, message, args); });
        }

        /// <summary>
        ///     记录调试信息
        /// </summary>
        /// <param name="message"></param>
        public void LogDebug(string message)
        {
            LogDebug(message, null);
        }

        /// <summary>
        ///     记录信息
        /// </summary>
        /// <param name="message"></param>
        public void LogInformation(string message)
        {
            LogInformation(message, null);
        }
    }
}
