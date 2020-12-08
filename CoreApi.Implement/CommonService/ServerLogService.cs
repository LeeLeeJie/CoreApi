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
        protected override ILogger CurrentLogger => ServerRunTime.ServiceProvider?.GetService<ILoggerFactory>()
            .GetApplicationLogger("EMS服务器运行日志");
    }
}
