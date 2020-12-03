using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApi.Common.Tools
{
    public class JwtHelper
    {
        public static string IssueJwt(TokenModelJwt tokenModel)
        {
            //string iss = Appsettings.app(new string[] { "Audience", "Issuer" });
            return string.Empty;
        }
    }
    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModelJwt
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Uid { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 职能
        /// </summary>
        public string Work { get; set; }

    }
}
