using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CoreApi.Common.Extension;

namespace CoreApi.Common.Tools
{
    public class ConstantDefine
    {
        public static string ConfigPath => $"{Path.Combine(PathExtension.GetExecutingPath(), "Configs")}";

        public static string AppSettingPath => PathExtension.GetExecutingPath();
    }
}
