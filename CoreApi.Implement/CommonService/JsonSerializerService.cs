using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using CoreApi.IService.ICommonService;
using Newtonsoft.Json.Serialization;

namespace CoreApi.Service.CommonService
{
    public class JsonSerializerService : IJsonSerializer
    {
        public string ToJson(object instance)
        {
            return JsonConvert.SerializeObject(instance);
        }

        /// <summary>
        ///     将对象序列化成json字符串
        /// </summary>
        /// <param name="instance">用于序列化的对象实例</param>
        /// <param name="propertycamelCase">属性使用驼峰命名</param>
        /// <returns>对象序列化后的json字符串</returns>
        public string ToJson(object instance, bool propertycamelCase)
        {
            if (!propertycamelCase) return ToJson(instance);

            //设置序列化时key为驼峰样式  
            var camelSettings =
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    //                    Formatting       = Formatting.Indented
                };
            return JsonConvert.SerializeObject(instance, camelSettings);
        }

        /// <summary>
        ///     将对象序列化成嵌套结构的json字符串
        /// </summary>
        /// <param name="instance">用于序列化的对象实例</param>
        /// <returns>对象序列化后的json字符串</returns>
        public string ToNestedJson(object instance)
        {
            return JsonConvert.SerializeObject(instance, Formatting.Indented);
        }

        public string ToJson(object instance, Type type)
        {
            return JsonConvert.SerializeObject(instance, type, null);
        }

        /// <summary>
        ///     将对象序列化成json字符串
        /// </summary>
        /// <param name="instance">用于序列化的对象实例</param>
        /// <param name="type">对象形式类型</param>
        /// <param name="propertycamelCase">属性使用驼峰命名</param>
        /// <returns>对象序列化后的json字符串</returns>
        public string ToJson(object instance, Type type, bool propertycamelCase)
        {
            if (!propertycamelCase) return ToJson(instance, type);

            //设置序列化时key为驼峰样式  
            var camelSettings =
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented
                };
            return JsonConvert.SerializeObject(instance, type, camelSettings);
        }
        public object FromJson(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }

        public T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
