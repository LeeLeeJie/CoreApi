using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CoreApi.Common.Tools;
using CoreApi.Interface.ICommonService;
using OperateResult = CoreApi.Interface.CommonEntities.OperateResult;

namespace CoreApi.Implement.CommonService
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

        public bool IsWeb = false;

        protected string ConfigDirectoryPath
        {
            get
            {
                if (IsWeb)
                    return $"{Path.Combine(AppDomain.CurrentDomain.RelativeSearchPath, "Configs")}";
                else
                    return ConstantDefine.ConfigPath;
            }
        }

        protected string ConfigFileFullPath
        {
            get
            {
                if (IsWeb)
                    return $"{Path.Combine(AppDomain.CurrentDomain.RelativeSearchPath, "Configs", ConfigFileName)}.json";
                else
                    return $"{Path.Combine(ConstantDefine.ConfigPath, ConfigFileName)}.json";
            }
        }


        /// <summary>
        ///     配置文件名
        /// </summary>
        public abstract string ConfigFileName { get; }

        /// <summary>
        ///     读取配置集
        /// </summary>
        /// <returns></returns>
        public virtual Interface.CommonEntities.OperateResult<TConfig> ReadConfig()
        {
            try
            {
                if (!File.Exists(ConfigFileFullPath))
                    return new Interface.CommonEntities.OperateResult<TConfig>(false, $"文件不存在：【{ConfigFileFullPath}】");

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

                return new Interface.CommonEntities.OperateResult<TConfig>(current);
            }
            catch (Exception e)
            {
                LogService?.LogError(e, e.Message, null);
                return new Interface.CommonEntities.OperateResult<TConfig>(e);
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
