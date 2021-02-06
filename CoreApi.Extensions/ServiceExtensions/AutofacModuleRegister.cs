using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using CoreApi.Common;
using CoreApi.Extensions.AOP;
using CoreApi.Extensions.Cache;
using CoreApi.IService.ICommonService;
using CoreApi.Repository;
using CoreApi.Service.CommonService;

namespace CoreApi.Extensions.ServiceExtensions
{
    public class AutofacModuleRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var basePath = AppContext.BaseDirectory;
            var repositoryDllFile = Path.Combine(basePath, "CoreApi.Repository.dll");
            var servicesDllFile = Path.Combine(basePath, "CoreApi.Service.dll");

            // AOP 开关，如果想要打开指定的功能，只需要在 appsettigns.json 对应对应 true 就行。
            var cacheType = new List<Type>();
            //if (Appsettings.app(new string[] { "AppSettings", "RedisCachingAOP", "Enabled" }).ObjToBool())
            //{
            //    builder.RegisterType<BlogRedisCacheAOP>();
            //    cacheType.Add(typeof(BlogRedisCacheAOP));
            //}
            //if (Appsettings.app(new string[] { "AppSettings", "MemoryCachingAOP", "Enabled" }).ObjToBool())
            //{
            //    builder.RegisterType<BlogCacheAOP>();
            //    cacheType.Add(typeof(BlogCacheAOP));
            //}
            //if (Appsettings.app(new string[] { "AppSettings", "TranAOP", "Enabled" }).ObjToBool())
            //{
            //    builder.RegisterType<BlogTranAOP>();
            //    cacheType.Add(typeof(BlogTranAOP));
            //}
            //if (Appsettings.app(new string[] { "AppSettings", "LogAOP", "Enabled" }).ObjToBool())
            //{
            //    builder.RegisterType<BlogLogAOP>();
            //    cacheType.Add(typeof(BlogLogAOP));
            //}

            //containerBuilder.RegisterType<MemoryCaching>().As<ICaching>().InstancePerLifetimeScope();
            //可以直接替换其他拦截器！一定要把拦截器进行注册
            containerBuilder.RegisterType<LogAop>();
            containerBuilder.RegisterType<CacheAOP>();
            containerBuilder.RegisterGeneric(typeof(BaseRepositoryService<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();//注册仓储
            containerBuilder.RegisterType(typeof(AppSettingConfigService)).As(typeof(IJsonConfigService<AppSettingModel>)).SingleInstance();//注册读取配置文件服务
            containerBuilder.RegisterType<ServerLogService>().As<ILogService>().SingleInstance();

            


            // 获取 Service.dll 程序集服务，并注册
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            containerBuilder.RegisterAssemblyTypes(assemblysServices)
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;
                //.InterceptedBy(cacheType.ToArray());//允许将拦截器服务的列表分配给注册。
                .InterceptedBy(typeof(LogAop),typeof(CacheAOP));//允许将拦截器服务的列表分配给注册。

            // 获取 Repository.dll 程序集服务，并注册
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            containerBuilder.RegisterAssemblyTypes(assemblysRepository)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            //1、瞬时生命周期：注册之后，每次获取到的服务实例都不一样（默认的注册方式）
            //containerBuilder.RegisterType<UserService>().As<>().InstancePerDependency();
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

            //#region V2
            ////Autofac 基于配置文件的服务注册
            //IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
            //configurationBuilder.AddJsonFile($@"Config/autofac.json");
            //IConfigurationRoot root = configurationBuilder.Build();
            ////开始读取配置文件中的内容
            //ConfigurationModule module = new ConfigurationModule(root);
            ////根据配置文件的内容注册服务
            //containerBuilder.RegisterModule(module);
            ////aotuofac服务注册完成回调
            //containerBuilder.RegisterBuildCallback(lifetimeScope =>
            //{
            //    ServerRunTime.ServiceContainer = (IContainer)lifetimeScope;
            //});
            //#endregion
        }
    }
}
