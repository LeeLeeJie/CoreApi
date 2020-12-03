using System;
using System.Collections.Generic;
using System.Text;
using CoreApi.Common.Tools;

namespace CoreApi.Common.ICommonService
{
    public interface IServiceCore
    {
        string ServiceDescription { get; }

        OperateResult StartService();

        OperateResult StopService();
    }
}
