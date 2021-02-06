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
            Host.CreateDefaultBuilder(args)//����һ��Ĭ�ϵ�ͨ������Host������
                .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //ʹ��autofac�����������滻ϵͳĬ�ϵ�����
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    //ע��Ӧ�ó�������ʹ�õ������ļ����������ݿ����ӵȵ�
                    //config.SetBasePath(Directory.GetCurrentDirectory());
                    //config.AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices(service =>
                {
                    //ע������м���Ȳ���
                    Console.WriteLine("ConfigureServices");
                })
                .ConfigureLogging((ILoggingBuilder logBuilder) =>
                {
                    //������־
                    logBuilder.AddNLog();
                    logBuilder.AddConsole();
                    NLog.LogManager.LoadConfiguration("Config/NLog.config");
                })
                .ConfigureHostConfiguration(builder => {
                    //����ʱ��Ҫ��������õȣ���������Ķ˿� url��ַ��
                    Console.WriteLine("ConfigureHostCOnfiguration");
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

    }
}
