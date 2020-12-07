using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using CoreApi.Model.ConfigModel;
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            //1、瞬时生命周期：注册之后，每次获取到的服务实例都不一样（默认的注册方式）
            //containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();
            //2、单例生命周期：整个容器中获取的服务实例都是同一个
            //containerBuilder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            //3、作用域生命周期：在相同作用域下获取到的服务实例是相同的
            //containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            //4、作用域生命周期：可以指定到某一个作用域，然后在相同作用域下共享服务实例
            //containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerMatchingLifetimeScope("My");
            //5、http请求上下文的生命周期：在一次Http请求上下文中,共享一个组件实例。仅适用于asp.net mvc开发。
            //containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            //6、拥有隐式关系类型的创建新的嵌套生命周期的作用域，在一个生命周期域中所拥有的实例创建的生命周期中，
            //      每一个依赖组件或调用Resolve()方法创建一个单一的共享的实例，并且子生命周期域共享父生命周期域中的实例
            //containerBuilder.RegisterType<UserService>().InstancePerOwned<IUserService>();

            #region 手动注册模式，耦合度太高
            //containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope().AsImplementedInterfaces();
            //containerBuilder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope().AsImplementedInterfaces();
            //var container = containerBuilder.Build();
            //IUserService userService = container.Resolve<IUserService>();
            //IProductService productService = container.Resolve<IProductService>();
            //userService.Show();
            //productService.Show();
            #endregion

            #region 反射方式注入
            //Assembly service = Assembly.Load("AspNetCore.Ioc.Service");
            //Assembly iservice = Assembly.Load("AspNetCore.Ioc.Interface");
            //containerBuilder.RegisterAssemblyTypes(service, iservice)
            //    .Where(t => t.FullName.EndsWith("Service") && !t.IsAbstract) //类名以service结尾，且类型不能是抽象的　
            //    .InstancePerLifetimeScope() //生命周期，，
            //    .AsImplementedInterfaces()
            //    .PropertiesAutowired(); //属性注入
            #endregion

            #region 简化反射方式注入
            containerBuilder.RegisterModule<MyAutofacModule>();
            #endregion
        }
    }
}
