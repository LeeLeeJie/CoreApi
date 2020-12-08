using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApi.Common
{
    public class ServerRunTime
    {
        /// <summary>
        ///     服务容器
        /// </summary>
        public static IServiceProvider ServiceProvider { get; set; }
    }
}
