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
using Newtonsoft.Json;
using System.Linq;
using UploadPB.Services.Interfaces.IPostBC30Service.PostAG;

namespace UploadPB.Controller.Post.PostAG
{
    public class PostBeacukaiController : ControllerBase
    {

        public IPostBeacukai30 _postBeacukai30;


        public PostBeacukaiController(IPostBeacukai30 postBeacukai30)
        {
            _postBeacukai30 = postBeacukai30;
        }

        [FunctionName("PostBeacukaies-AG")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post","put", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            var Username = req.Headers["username"].FirstOrDefault();

            var content = await new StreamReader(req.Body).ReadToEndAsync();
            List<TemporaryViewModel> Data = JsonConvert.DeserializeObject<List<TemporaryViewModel>>(content);
            try
            {
                if(Data.Count > 0)
                {
                    
                    if (Data[0].Type == "30")
                    {
                        await _postBeacukai30.PostBeacukai(Data, Username);
                    }
                   
                }
                    
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new ResponseFailed(ex.Message));
            }      
            
            return new OkObjectResult(new ResponseSuccess("Berhasil Menyimpan Data Temporary"));
        }
    }
}
