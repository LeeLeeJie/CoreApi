﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using CoreApi.Common;
using CoreApi.Common.Tools;
using CoreApi.Service.CommonService;
using CoreApi.IService.ICommonService;

namespace CoreApi.Service.CommonService
{
    public class AppSettingConfigService: JsonConfigBaseService<AppSettingModel>
    {
        #region Ctor
        public AppSettingConfigService(ILogService logService, IJsonSerializer jsonSerializer)
            : base(logService, jsonSerializer)
        {
        }
        #endregion

        public override string ServiceDescription => "客户端配置服务";

        /// <summary>
        ///     配置文件名
        /// </summary>
        public override string ConfigFileName => "Appsettings";

        /// <summary>
        ///     配置文件名
        /// </summary>
        public override string ConfigPath => ConstantDefine.AppSettingPath;
    }
}
