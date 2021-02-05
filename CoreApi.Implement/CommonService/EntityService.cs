using System;
using System.Collections.Generic;
using System.Text;
using CoreApi.Common;
using CoreApi.IService.ICommonService;
using CoreApi.Repository;
using Microsoft.Extensions.Configuration;
using SqlSugar;

namespace CoreApi.Service.CommonService
{
    public class EntityService : IEntity
    {
        public SqlSugarClient db;

        public EntityService(IConfiguration configuration)
        {
            DbContext.Init(configuration.GetSection(nameof(AppSettingModel.DatabaseConfig)).Get<DatabaseConfigModel>());
            db = DbContext.GetDbContext().Db;
        }
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public OperateResult CreateEntity(string entityName, string filePath)
        {
            try
            {
                db.DbFirst.IsCreateAttribute().Where(entityName).CreateClassFile(filePath);
                return new OperateResult();
            }
            catch (Exception ex)
            {
                return new OperateResult(ex);
            }
        }

        /// <summary>
        /// 根据实体生成数据表
        /// </summary>
        /// <param name="entityTypes"></param>
        /// <returns></returns>
        public OperateResult CreateDBTableByEntity(Type[] entityTypes)
        {
            try
            {
                db.CodeFirst.InitTables(entityTypes);
                return new OperateResult();
            }
            catch (Exception ex)
            {
                return new OperateResult(ex);
            }
        }
    }
}
