using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CoreApi.Common;
using CoreApi.IService.ICommonService;
using CoreApi.IService.IEnityService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiTestController : Controller
    {

        private readonly IJsonSerializer JsonSerializerService = null;

        private readonly IJsonConfigService<AppSettingModel> ConfigService;

        private readonly ILogService LogService;
        public ApiTestController(IJsonSerializer _jsonSerializer, ILogService logService, IJsonConfigService<AppSettingModel> configService)
        {
            JsonSerializerService = _jsonSerializer;
            LogService = logService;
            ConfigService = configService;
        }

        /// <summary>
        /// Swagger接口测试
        /// </summary>
        /// <param name="loginName">用户名</param>
        /// <returns></returns>
        [HttpGet("Login")]
        public IActionResult Login(string loginName)
        {
            //RSAHelper rsaHelper = new RSAHelper(RSAType.RSA,Encoding.Default);
            //string tempStr = rsaHelper.Encrypt("www.baidu.com");
            //string resDecrypt = rsaHelper.Decrypt(tempStr);
            var settingConfig = ConfigService?.Current;
            LogService.LogInformation($"LogService-{loginName}");
            //string aa = JsonSerializerService.ToJson(new JwtAuthConfigModel());
            string aa = ServerRunTime.ServiceContainer.Resolve<IJsonSerializer>()?.ToJson(new JwtAuthConfigModel());
            return Ok(aa);
        }
        /// <summary>
        /// 生成Https证书
        /// </summary>
        /// <returns></returns>
        [HttpGet("Test2")]
        public IActionResult Test2()
        {
            //var alarmDataService = ServerRunTime.ServiceContainer.Resolve<IAlarmDataService>();
            //List<AlarmData>  alarmDatas = alarmDataService.Query().Result;
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            var bcuDataService = ServerRunTime.ServiceContainer.Resolve<IBcuDataService>();
            List<BcuData>  bcuDatas = bcuDataService.Query().Result;
            stopwatch2.Stop();
            return Ok(new OperateResult<long>(stopwatch2.ElapsedMilliseconds));
        }


        /// <summary>
        /// 生成实体
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        [HttpGet("CreateEntity")]
        public IActionResult CreateEntity(string entityName)
        {
            if(entityName == null) 
                return Json("参数为空");
            var entityService = ServerRunTime.ServiceContainer.Resolve<IEntity>();
            string filePath = Directory.GetCurrentDirectory();
            filePath = filePath.Substring(0, filePath.LastIndexOf('\\')) + "\\" + "CoreApi.Model\\DBEntity";
            var res = entityService?.CreateEntity(entityName, filePath);
            return Json(new OperateResult());
        }

        /// <summary>
        /// 根据实体生成表
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        [HttpGet("CreateDBTableByEntity")]
        public IActionResult CreateDBTableByEntity(string entityName)
        {
            if (entityName == null) return Json("参数为空");
            //var res = EntityService?.CreateDBTableByEntity(new Type[]{ typeof(Test001) });
            return Json(new OperateResult());
        }

    }
}
