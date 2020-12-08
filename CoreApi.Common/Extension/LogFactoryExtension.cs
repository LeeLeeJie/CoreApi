using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace CoreApi.Common.Extension
{
    public static class LogFactoryExtension
    {
        /// <summary>
        ///     获取整个应用程序日志记录器
        /// </summary>
        /// <param name="factory">日志记录器工厂</param>
        /// <param name="typeName"></param>
        /// <returns>日志记录器</returns>
        public static ILogger GetApplicationLogger(this ILoggerFactory factory, string typeName)
        {
            return factory.CreateLogger(typeName);
        }
    }
}
