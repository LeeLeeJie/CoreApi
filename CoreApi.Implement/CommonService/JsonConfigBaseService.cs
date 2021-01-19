using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CoreApi.Common.Tools;
using CoreApi.IService.ICommonService;
using CoreApi.IService.CommonEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CoreApi.Service.CommonService
{
    public abstract class JsonConfigBaseService<TConfig> : IJsonConfigService<TConfig>
        where TConfig : IConfig, new()
    {
        protected TConfig current;

        /// <summary>
        /// 配置文件内容
        /// </summary>
        public TConfig Current
        {
            get
            {
                if (current == null)
                    current = ReadConfig().ReturnValue;

                return current;
            }
        }

        #region Ctor

        protected JsonConfigBaseService(ILogService logService, IJsonSerializer jsonSerializer)
        {
            LogService = logService;
            JsonSerializer = jsonSerializer;
        }

        #endregion

        protected string ConfigDirectoryPath
        {
            get
            {
                return ConfigPath;
            }
        }

        protected string ConfigFileFullPath
        {
            get
            {
                return $"{Path.Combine(ConfigPath, ConfigFileName)}.json";
            }
        }


        /// <summary>
        ///     配置文件名
        /// </summary>
        public abstract string ConfigFileName { get; }

        /// <summary>
        ///     配置文件名
        /// </summary>
        public virtual string ConfigPath => ConstantDefine.ConfigPath;

        /// <summary>
        ///     读取配置集
        /// </summary>
        /// <returns></returns>
        public virtual OperateResult<TConfig> ReadConfig()
        {
            try
            {
                if (!File.Exists(ConfigFileFullPath))
                    return new OperateResult<TConfig>(false, $"文件不存在：【{ConfigFileFullPath}】");

                var sr = new StreamReader(ConfigFileFullPath, Encoding.UTF8);
                var fileContent = sr.ReadToEnd();
                sr.Close();
                var cfgObj = JsonSerializer.FromJson<TConfig>(fileContent);
                if (string.IsNullOrEmpty(fileContent))
                {
                    var tConfig = new TConfig();
                    WriteConfig(tConfig);
                    current = tConfig;
                }
                else
                    current = cfgObj;

                return new OperateResult<TConfig>(current);
            }
            catch (Exception e)
            {
                LogService?.LogError(e, e.Message, null);
                return new OperateResult<TConfig>(e);
            }
        }

        /// <summary>
        ///     写入配置集
        /// </summary>
        /// <param name="appCfg"></param>
        /// <returns></returns>
        public virtual OperateResult WriteConfig(TConfig appCfg)
        {
            if (appCfg == null) return new OperateResult(false, $"{nameof(appCfg)}为空");
            try
            {
                if (!Directory.Exists(ConfigDirectoryPath))
                    Directory.CreateDirectory(ConfigDirectoryPath);
                var sw = new StreamWriter(ConfigFileFullPath, false, Encoding.UTF8);
                sw.WriteLine(JsonSerializer.ToNestedJson(appCfg));
                sw.Close();
                current = appCfg;
                return new OperateResult();
            }
            catch (Exception e)
            {
                LogService?.LogError(e, e.Message, null);
                return new OperateResult(e);
            }
        }

        /// <summary>
        ///     服务描述
        /// </summary>
        public abstract string ServiceDescription { get; }

        /// <summary>
        ///     启动服务
        /// </summary>
        /// <returns></returns>
        public virtual OperateResult StartService()
        {
            try
            {
                var fullFilePath = $"{Path.Combine(ConstantDefine.ConfigPath, ConfigFileName)}.json";
                if (!File.Exists(fullFilePath))
                {
                    var cfgObj = new TConfig();
                    WriteConfig(cfgObj);
                }

                return new OperateResult();
            }
            catch (Exception e)
            {
                LogService?.LogError(e, e.Message, null);
                return new OperateResult(e);
            }
        }

        /// <summary>
        ///     停止服务
        /// </summary>
        /// <returns></returns>
        public virtual OperateResult StopService()
        {
            return new OperateResult();
        }
        #region Parameters

        protected readonly IJsonSerializer JsonSerializer;
        protected readonly ILogService LogService;

        #endregion
    }
}
