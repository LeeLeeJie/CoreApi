using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace CoreApi.Common
{
    public class AppSettingModel
    {
        /// <summary>
        /// Jwt配置
        /// </summary>
        public JwtAuthConfigModel JwtAuthConfig;
        /// <summary>
        /// 数据库配置
        /// </summary>
        public DatabaseConfigModel DatabaseConfig;
    }

    public class JwtAuthConfigModel
    {
        /// <summary>
        /// 加密密钥
        /// </summary>
        public string JWTSecretKey = "This is JWT Secret Key";

        /// <summary>
        /// Web端过期时间
        /// </summary>
        public int WebExp { get; set; } = 12;

        /// <summary>
        /// app端过期时间
        /// </summary>
        public int AppExp { get; set; } = 12;
        /// <summary>
        /// 小程序端过期时间
        /// </summary>
        public int MiniProgramExp { get; set; } = 12;
        /// <summary>
        /// 其他应用过期时间
        /// </summary>
        public int OtherExp { get; set; } = 12;

        /// <summary>
        /// 签发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public string Audience { get; set; }
    }

    public class DatabaseConfigModel
    {
        public string ConnectionString { get; set; }
        public DbType DbType { get; set; } = DbType.SqlServer;
    }
}
