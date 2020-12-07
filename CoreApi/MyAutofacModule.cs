using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;

namespace CoreApi
{
    public class MyAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region V1
            //反射程序集方式服务注册
            //Assembly service = Assembly.Load("AspNetCore.Ioc.Service");
            //Assembly iservice = Assembly.Load("AspNetCore.Ioc.Interface");
            //builder.RegisterAssemblyTypes(service, iservice)
            //    .Where(t => t.FullName.EndsWith("Service") && !t.IsAbstract) //类名以service结尾，且类型不能是抽象的　
            //    .InstancePerLifetimeScope() //作用域生命周期
            //    .AsImplementedInterfaces()
            //    .PropertiesAutowired(); //属性注入 
            #endregion

            #region V2
            //Autofac 基于配置文件的服务注册
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("Config/autofac.json");
            IConfigurationRoot root = configurationBuilder.Build();
            //开始读取配置文件中的内容
            ConfigurationModule module = new ConfigurationModule(root);
            //根据配置文件的内容注册服务
            builder.RegisterModule(module);
            #endregion
        }
    }
}
