using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace CoreApi.Common
{
    public class ServerRunTime
    {
        /// <summary>
        ///     服务容器
        /// </summary>
        public static IContainer ServiceContainer { get; set; }
    }
}
