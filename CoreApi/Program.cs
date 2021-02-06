using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace CoreApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)//开启一个默认的通用主机Host建造者
                .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //使用autofac的容器工厂替换系统默认的容器
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    //注册应用程序内所使用的配置文件，比如数据库链接等等
                    //config.SetBasePath(Directory.GetCurrentDirectory());
                    //config.AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices(service =>
                {
                    //注册服务中间件等操作
                    Console.WriteLine("ConfigureServices");
                })
                .ConfigureLogging((ILoggingBuilder logBuilder) =>
                {
                    //配置日志
                    logBuilder.AddNLog();
                    logBuilder.AddConsole();
                    NLog.LogManager.LoadConfiguration("Config/NLog.config");
                })
                .ConfigureHostConfiguration(builder => {
                    //启动时需要的组件配置等，比如监听的端口 url地址等
                    Console.WriteLine("ConfigureHostCOnfiguration");
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

    }
}
