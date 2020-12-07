using System;
using System.Collections.Generic;
using System.Text;
using CoreApi.Interface.CommonEntities;
using CoreApi.Interface.ICommonService;

namespace CoreApi.Implement.CommonService
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
    }
}
