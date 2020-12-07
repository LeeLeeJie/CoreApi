using System;
using System.Collections.Generic;
using System.Text;
using CoreApi.Interface.CommonEntities;

namespace CoreApi.Interface.ICommonService
{
    public interface IServiceCore
    {
        string ServiceDescription { get; }

        OperateResult StartService();

        OperateResult StopService();
    }
}
