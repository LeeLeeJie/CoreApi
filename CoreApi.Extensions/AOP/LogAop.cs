using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using CoreApi.IService.ICommonService;

namespace CoreApi.Extensions.AOP
{
    /// <summary>
    /// 日志拦截器,继承IInterceptor接口
    /// </summary>
    public class LogAop : IInterceptor
    {
        //private readonly ILogService LogService;
        //public LogAop(ILogService logService)
        //{
        //    LogService = logService;
        //}
        public void Intercept(IInvocation invocation)
        {
            //step1 事前处理：在服务方法执行前，做相应的逻辑处理
            var dataIntercept = "" +
                                $"【当前执行方法】：{ invocation.Method.Name} \r\n" +
                                $"【携带的参数有】： {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())} \r\n";
            //step2 执行当前访问的服务方法,(注意:如果下边还有其他的AOP拦截器的话,会跳转到其他的AOP里)
            invocation.Proceed();
            //step3 事后处理: 在service被执行了以后,做相应的处理,这里是输出到日志文件
            dataIntercept += ($"【执行完成结果】：{invocation.ReturnValue}");
            //step4 输出到日志文件
            Parallel.For(0, 1, e => { Console.WriteLine(dataIntercept); });
        }
    }
}
