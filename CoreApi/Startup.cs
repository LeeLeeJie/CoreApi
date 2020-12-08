using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CoreApi.IService.ICommonService;
using CoreApi.Model.ConfigModel;
using CoreApi.Service.CommonService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace CoreApi
{
    public class Startup
    {
        public string ApiName { get; set; } = "CoreApi";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region Swagger配置
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = $"{ApiName} 接口文档——Netcore 3.1",
                });
                c.OrderActionsBy(o => o.RelativePath);
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath, "CoreApi.xml");
                    c.IncludeXmlComments(xmlPath);
                //接口参数描述
                //var xmlModelPath = Path.Combine(basePath, "CoreApi.Common.xml");//这个就是Model层的xml文件名
                //c.IncludeXmlComments(xmlModelPath);

            });
            #endregion
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            #region Swagger配置
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"{ApiName} V1");

                //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉，如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "doc";
                c.RoutePrefix = "";
            });
            #endregion
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                //默认路由
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //pattern: "{controller=Demo}/{action=Index}/{id?}");

                //区域路由(要放在默认路由的后面)
                //注：必须以特性的形式在对应控制器上加上区域名称 [Area("XXXX")]
                endpoints.MapControllerRoute(
                    name: "default2",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
        /// <summary>
        /// autofac注入,在ConfigureServices后运行
        /// </summary>
        /// <param name="containerBuilder"></param>
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            #region 简化反射方式注入
            containerBuilder.RegisterModule<MyAutofacModule>();
            #endregion
        }
    }
}
