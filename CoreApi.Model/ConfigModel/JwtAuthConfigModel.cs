using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApi.Model.ConfigModel
{
    public class JwtAuthConfigModel : BaseConfigModel
    {
        public JwtAuthConfigModel()
        {
            try
            {
                JWTSecretKey = Configuration["JwtAuth:SecurityKey"];
                WebExp = int.Parse(Configuration["JwtAuth:WebExp"]);
                AppExp = int.Parse(Configuration["JwtAuth:AppExp"]);
                MiniProgramExp = int.Parse(Configuration["JwtAuth:MiniProgramExp"]);
                OtherExp = int.Parse(Configuration["JwtAuth:OtherExp"]);
                Issuer = Configuration["JwtAuth:Issuer"];
                Audience = Configuration["JwtAuth:Audience"];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
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
}
