using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CoreApi.Common.Tools.Encryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiTestController : ControllerBase
    {
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

            //string MakeCert = "C:\\Program Files\\Microsoft Visual Studio 8\\SDK\\v2.0\\Bin\\makecert.exe";
            string MakeCert = @"C:\Program Files (x86)\Windows Kits\10\bin\10.0.17763.0\x64\makecert.exe";
            string x509Name = "CN=zhkotron";
            string param = " -pe -ss my -n \"" + x509Name + "\" ";
            Process p = Process.Start(MakeCert, param);
            p.StartInfo.Verb = "runas";
            p.WaitForExit();
            //p.StandardInput.WriteLine("exit");
            //string strRst = p.StandardOutput.ReadToEnd();
            p.Close();
            return Ok();
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
        ///// <summary>
        ///// 生成Https证书
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public IActionResult CreatePfxFile(string loginName)
        //{
        //    //string MakeCert = "C:\\Program Files\\Microsoft Visual Studio 8\\SDK\\v2.0\\Bin\\makecert.exe";
        //    //string MakeCert = @"C:\Program Files (x86)\Windows Kits\10\bin\10.0.18362.0\x64\makecert.exe";
        //    //string x509Name = "CN=zhkotron";
        //    //string param = " -pe -ss my -n \"" + x509Name + "\" ";
        //    //Process p = Process.Start(MakeCert, param);
        //    //p.WaitForExit();
        //    //p.Close();


        //    //X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        //    //store.Open(OpenFlags.ReadWrite);
        //    //X509Certificate2Collection storecollection = (X509Certificate2Collection)store.Certificates;
        //    //foreach (X509Certificate2 x509 in storecollection)
        //    //{
        //    //    if (x509.Subject == "CN=zhkotron")
        //    //    {
        //    //        Debug.Print(string.Format("certificate name: {0}", x509.Subject));
        //    //        byte[] pfxByte = x509.Export(X509ContentType.Pfx, "123");
        //    //        using (FileStream fileStream = new FileStream("luminji.pfx", FileMode.Create))
        //    //        {
        //    //            // Write the data to the file, byte by byte.   
        //    //            for (int i = 0; i < pfxByte.Length; i++)
        //    //                fileStream.WriteByte(pfxByte[i]);
        //    //            // Set the stream position to the beginning of the file.   
        //    //            fileStream.Seek(0, SeekOrigin.Begin);
        //    //            // Read and verify the data.   
        //    //            for (int i = 0; i < fileStream.Length; i++)
        //    //            {
        //    //                if (pfxByte[i] != fileStream.ReadByte())
        //    //                {
        //    //                    Debug.Print("Error writing data.");
        //    //                    return;
        //    //                }
        //    //            }
        //    //            fileStream.Close();
        //    //            Debug.Print("The data was written to {0} " +
        //    //                "and verified.", fileStream.Name);
        //    //        }
        //    //        string myname = "my name is luminji! and i love huzhonghua!";
        //    //        //string enStr = this.RSAEncrypt(x509.PublicKey.Key.ToXmlString(false), myname);
        //    //        //string deStr = this.RSADecrypt(x509.PrivateKey.ToXmlString(true), enStr);
        //    //    }
        //    //}
        //    //store.Close();
        //    //store = null;
        //    //storecollection = null;

        //    return Ok();
        //}
    }
}
