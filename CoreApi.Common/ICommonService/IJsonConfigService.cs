﻿using System;
using System.Collections.Generic;
using System.Text;
using CoreApi.Common.Tools;

namespace CoreApi.Common.ICommonService
{
    public interface IJsonConfigService<TConfig> : IServiceCore
        where TConfig : IConfig
    {
        /// <summary>
        /// 配置文件内容
        /// </summary>
        TConfig Current { get; }

        /// <summary>
        ///     配置文件名
        /// </summary>
        string ConfigFileName { get; }

        /// <summary>
        ///     读取配置集
        /// </summary>
        /// <returns></returns>
        OperateResult<TConfig> ReadConfig();

        /// <summary>
        ///     写入配置集
        /// </summary>
        /// <param name="configData"></param>
        /// <returns></returns>
        OperateResult WriteConfig(TConfig configData);
    }
}
