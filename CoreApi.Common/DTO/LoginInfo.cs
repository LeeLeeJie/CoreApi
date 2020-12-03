using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApi.Common.DTO
{
    public class LoginInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
    }
}
