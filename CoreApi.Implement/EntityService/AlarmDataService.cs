using System;
using System.Collections.Generic;
using System.Text;
using CoreApi.IService.IEnityService;
using CoreApi.Repository;
using Models;

namespace CoreApi.Service.EntityService
{
    public class AlarmDataService : EntityBaseService<AlarmData>, IAlarmDataService
    {
        IBaseRepository<AlarmData> _dal;
        public AlarmDataService(IBaseRepository<AlarmData> dal):base(dal)
        {
            this._dal = dal;
        }
    }
}
