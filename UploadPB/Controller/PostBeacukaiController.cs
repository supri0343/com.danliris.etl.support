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
using UploadPB.Services.Interfaces.IPostBC23;
using UploadPB.Services.Class;
using Newtonsoft.Json;
using System.Linq;


namespace UploadPB.Controller
{
    public class PostBeacukaiController : ControllerBase
    {
        public IPostBeacukai _postBeacukai;

        public PostBeacukaiController(IPostBeacukai postBeacukai, IServiceProvider serviceProvider)
        {
            _postBeacukai = postBeacukai;
        }

        [FunctionName("PostBeacukai")]
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
                    await _postBeacukai.PostBeacukai(Data, Username);
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult(new ResponseFailed(ex.Message));
                }      
            
            return new OkObjectResult(new ResponseSuccess("Berhasil Menyimpan Data Temporary"));
        }
    }
}
