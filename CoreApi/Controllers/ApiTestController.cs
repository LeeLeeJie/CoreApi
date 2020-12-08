﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CoreApi.Common.Tools.Encryption;
using CoreApi.IService.CommonEntities;
using CoreApi.IService.ICommonService;
using CoreApi.Service.CommonService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiTestController : ControllerBase
    {
        private readonly IJsonSerializer JsonSerializerService = null;

        private readonly ILogService LogService;

        private readonly ILifetimeScope Container;

        public ApiTestController(IJsonSerializer _jsonSerializer, ILogService logService,
            ILifetimeScope lifetimeScope)
        {
            JsonSerializerService = _jsonSerializer;
            LogService = logService;
            Container = lifetimeScope;
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
            LogService.LogInformation(loginName);
            //string aa = JsonSerializerService.ToJson(new JwtAuthConfigModel());
            string aa = Container?.Resolve<IJsonSerializer>()?.ToJson(new JwtAuthConfigModel());
            return Ok(aa);
        }
        /// <summary>
        /// 生成Https证书
        /// </summary>
        /// <returns></returns>
        [HttpGet("Test2")]
        public IActionResult Test2()
        {
            return Ok();
        }
        
    }
}
