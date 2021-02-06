using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using CoreApi.Extensions.Cache;

namespace CoreApi.Extensions.AOP
{
    /// <summary>
    /// 面向切面的缓存使用
    /// </summary>
    public class CacheAOP: AOPbase
    {
        //通过注入的方式，把缓存操作接口通过构造函数注入
        private readonly ICaching _cache;
        public CacheAOP(ICaching cache)
        {
            _cache = cache;
        }
        public override void Intercept(IInvocation invocation)
        {
            //获取自定义缓存键
            var cacheKey = CustomCacheKey(invocation);
            //根据key获取相应的缓存值
            var cacheValue = _cache.Get(cacheKey);
            if (cacheValue != null)
            {
                //将当前获取到的缓存值，赋值给当前执行方法
                invocation.ReturnValue = cacheValue;
                return;
            }
            //去执行当前的方法
            invocation.Proceed();
            //存入缓存
            if (!string.IsNullOrWhiteSpace(cacheKey))
            {
                _cache.Set(cacheKey, invocation.ReturnValue);
            }
        }
    }
}
