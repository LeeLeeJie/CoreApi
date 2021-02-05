using System;
using System.Collections.Generic;
using System.Text;
using CoreApi.IService.IEnityService;
using CoreApi.Repository;
using Models;

namespace CoreApi.Service.EntityService
{
    public class BcuDataService : EntityBaseService<BcuData>, IBcuDataService
    {
        IBaseRepository<BcuData> _dal;
        public BcuDataService(IBaseRepository<BcuData> dal):base(dal)
        {
            this._dal = dal;
        }
    }
}
