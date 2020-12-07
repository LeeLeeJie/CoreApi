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

            #region Swagger����
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = $"{ApiName} �ӿ��ĵ�����Netcore 3.1",
                });
                c.OrderActionsBy(o => o.RelativePath);
                // Ϊ Swagger JSON and UI����xml�ĵ�ע��·��
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath, "CoreApi.xml");
                    c.IncludeXmlComments(xmlPath);
                //�ӿڲ�������
                //var xmlModelPath = Path.Combine(basePath, "CoreApi.Common.xml");//�������Model���xml�ļ���
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
            #region Swagger����
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"{ApiName} V1");

                //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�,ע��localhost:8001/swagger�Ƿ��ʲ����ģ�ȥlaunchSettings.json��launchUrlȥ����������뻻һ��·����ֱ��д���ּ��ɣ�����ֱ��дc.RoutePrefix = "doc";
                c.RoutePrefix = "";
            }); 
            #endregion
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                //Ĭ��·��
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //pattern: "{controller=Demo}/{action=Index}/{id?}");

                //����·��(Ҫ����Ĭ��·�ɵĺ���)
                //ע�����������Ե���ʽ�ڶ�Ӧ�������ϼ����������� [Area("XXXX")]
                endpoints.MapControllerRoute(
                    name: "default2",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            //1��˲ʱ�������ڣ�ע��֮��ÿ�λ�ȡ���ķ���ʵ������һ����Ĭ�ϵ�ע�᷽ʽ��
            //containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();
            //2�������������ڣ����������л�ȡ�ķ���ʵ������ͬһ��
            //containerBuilder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            //3���������������ڣ�����ͬ�������»�ȡ���ķ���ʵ������ͬ��
            //containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            //4���������������ڣ�����ָ����ĳһ��������Ȼ������ͬ�������¹������ʵ��
            //containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerMatchingLifetimeScope("My");
            //5��http���������ĵ��������ڣ���һ��Http������������,����һ�����ʵ������������asp.net mvc������
            //containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            //6��ӵ����ʽ��ϵ���͵Ĵ����µ�Ƕ���������ڵ���������һ����������������ӵ�е�ʵ�����������������У�
            //      ÿһ��������������Resolve()��������һ����һ�Ĺ����ʵ���������������������������������е�ʵ��
            //containerBuilder.RegisterType<UserService>().InstancePerOwned<IUserService>();

            #region �ֶ�ע��ģʽ����϶�̫��
            //containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope().AsImplementedInterfaces();
            //containerBuilder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope().AsImplementedInterfaces();
            //var container = containerBuilder.Build();
            //IUserService userService = container.Resolve<IUserService>();
            //IProductService productService = container.Resolve<IProductService>();
            //userService.Show();
            //productService.Show();
            #endregion

            #region ���䷽ʽע��
            //Assembly service = Assembly.Load("AspNetCore.Ioc.Service");
            //Assembly iservice = Assembly.Load("AspNetCore.Ioc.Interface");
            //containerBuilder.RegisterAssemblyTypes(service, iservice)
            //    .Where(t => t.FullName.EndsWith("Service") && !t.IsAbstract) //������service��β�������Ͳ����ǳ���ġ�
            //    .InstancePerLifetimeScope() //�������ڣ���
            //    .AsImplementedInterfaces()
            //    .PropertiesAutowired(); //����ע��
            #endregion

            #region �򻯷��䷽ʽע��
            containerBuilder.RegisterModule<MyAutofacModule>();
            #endregion
        }
    }
}
