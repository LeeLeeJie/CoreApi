using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CoreApi.Common.Extension
{
    public static class PathExtension
    {
        /// <summary>
        ///     获取执行文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetExecutingPath()
        {
            var directoryName = Assembly.GetEntryAssembly() == null
                ? Environment.CurrentDirectory
                : Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            if (Directory.Exists(directoryName)) return directoryName;

            throw new ApplicationException("获取执行文件路径异常");
        }
    }
}
