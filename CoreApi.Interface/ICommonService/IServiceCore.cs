using System;
using System.Collections.Generic;
using System.Text;
using CoreApi.Common;

namespace CoreApi.IService.ICommonService
{
    public interface IServiceCore
    {
        string ServiceDescription { get; }

        OperateResult StartService();

        OperateResult StopService();
    }
}
