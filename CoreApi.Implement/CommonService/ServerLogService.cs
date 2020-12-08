using System;
using System.Collections.Generic;
using System.Text;
using CoreApi.Common;
using CoreApi.Common.Extension;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoreApi.Service.CommonService
{
    public class ServerLogService: NLogBaseService
    {
        protected ILoggerFactory LoggerFactory;
        public ServerLogService(ILoggerFactory _loggerFactory)
        {
            LoggerFactory = _loggerFactory;
        }
        protected override ILogger CurrentLogger => LoggerFactory.GetApplicationLogger("服务器运行日志");
    }
}
