using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApi.Interface.ICommonService
{
    public interface IJsonSerializer
    {
        /// <summary>
        ///     将对象序列化成json字符串
        /// </summary>
        /// <param name="instance">用于序列化的对象实例</param>
        /// <returns>对象序列化后的json字符串</returns>
        string ToJson(object instance);

        /// <summary>
        ///     将对象序列化成嵌套结构的json字符串
        /// </summary>
        /// <param name="instance">用于序列化的对象实例</param>
        /// <returns>对象序列化后的json字符串</returns>
        string ToNestedJson(object instance);

        /// <summary>
        ///     将对象序列化成json字符串
        /// </summary>
        /// <param name="instance">用于序列化的对象实例</param>
        /// <param name="propertycamelCase">属性使用驼峰命名</param>
        /// <returns>对象序列化后的json字符串</returns>
        string ToJson(object instance, bool propertycamelCase);

        /// <summary>
        ///     将对象序列化成json字符串
        /// </summary>
        /// <param name="instance">用于序列化的对象实例</param>
        /// <param name="type">对象形式类型</param>
        /// <returns>对象序列化后的json字符串</returns>
        string ToJson(object instance, Type type);

        /// <summary>
        ///     将对象序列化成json字符串
        /// </summary>
        /// <param name="instance">用于序列化的对象实例</param>
        /// <param name="type">对象形式类型</param>
        /// <param name="propertycamelCase">属性使用驼峰命名</param>
        /// <returns>对象序列化后的json字符串</returns>
        string ToJson(object instance, Type type, bool propertycamelCase);

        /// <summary>
        ///     通过json字符串反序列化出对象
        /// </summary>
        /// <param name="json">用于反序列化的json字符串</param>
        /// <param name="type">对象形式类型</param>
        /// <returns>json字符串反序列化后的对象</returns>
        object FromJson(string json, Type type);

        /// <summary>
        ///     通过json字符串反序列化出对象
        /// </summary>
        /// <typeparam name="T">对象形式类型</typeparam>
        /// <param name="json">用于反序列化的json字符串</param>
        /// <returns>json字符串反序列化后的对象</returns>
        T FromJson<T>(string json);
    }
}
