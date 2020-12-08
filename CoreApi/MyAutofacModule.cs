using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using CoreApi.IService.CommonEntities;
using CoreApi.IService.ICommonService;
using CoreApi.Service.CommonService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoreApi
{
    public class MyAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
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

            containerBuilder.RegisterType<LoggerFactory>().As<ILoggerFactory>().SingleInstance();
            containerBuilder.RegisterType<ServerLogService>().As<ILogService>().SingleInstance();
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

            #region V2
            //Autofac 基于配置文件的服务注册
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile($@"Config/autofac.json");
            IConfigurationRoot root = configurationBuilder.Build();
            //开始读取配置文件中的内容
            ConfigurationModule module = new ConfigurationModule(root);
            //根据配置文件的内容注册服务
            containerBuilder.RegisterModule(module);
            #endregion
        }
    }
}
