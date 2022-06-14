using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using UploadPB.Models;
using UploadPB.ViewModels;
using UploadPB.Services.Interfaces;
using UploadPB.Services.Class;
using Newtonsoft.Json;
using System.Linq;


namespace UploadPB.Controller
{
    public class PostBeacukaiController : ControllerBase
    {
        public IServiceProvider ServiceProvider;
        private readonly IdentityService identityService;
        public IPostBeacukai _postBeacukai;

        public PostBeacukaiController(IPostBeacukai postBeacukai, IServiceProvider serviceProvider)
        {
            //_uploadExcel = uploadExcelsService;
            _postBeacukai = postBeacukai;
            identityService = (IdentityService)serviceProvider.GetService(typeof(IdentityService));

        }


        [FunctionName("PostBeacukai")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post","put", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            //req.HttpContext.Request.Headers.Add("Accept", "application/json");

            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);

            //var token = req.HttpContext.Response.Headers.ContainsKey("Authorization");

            //var token = req.Headers.ContainsKey("Authorization")

            //identityService.Username = 

            //var date = req.Form["revisedate"];
            //var ids = req.Form["data"];
            //identityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

            var content = await new StreamReader(req.Body).ReadToEndAsync();

                //var claims = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;

                //if (claims == null)
                //{
                //    return defaultValue;
                //}

                //var targetClaim = claims.FirstOrDefault(c => c.Type == "user_name");
                //if (targetClaim == null)
                //{
                //    return defaultValue;
                //}

                //return targetClaim.Value;

                //var revise2 = content.Contains
                //List<string> revise = JsonConvert.DeserializeObject<List<string>>(content);

                //identityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

                List<TemporaryViewModel> Data = JsonConvert.DeserializeObject<List<TemporaryViewModel>>(content);

                try
                {
                    await _postBeacukai.PostBeacukai(Data);
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult(new ResponseFailed(ex.Message));
                }
            
            
            return new OkObjectResult(new ResponseSuccess("Berhasil Menyimpan Data Temporary"));
        }
    }
}
