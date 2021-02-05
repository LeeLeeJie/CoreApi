using System;
using System.Collections.Generic;
using System.Text;
using CoreApi.Common;

namespace CoreApi.IService.ICommonService
{
    /// <summary>
    /// 实体数据接口
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        OperateResult CreateEntity(string entityName, string filePath);

        /// <summary>
        /// 根据实体生成数据表
        /// </summary>
        /// <param name="entityTypes"></param>
        /// <returns></returns>
        OperateResult CreateDBTableByEntity(Type[] entityTypes);
    }
}
