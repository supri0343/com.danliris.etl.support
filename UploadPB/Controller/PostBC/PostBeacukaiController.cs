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
using UploadPB.Services.Interfaces.Post40;
using Newtonsoft.Json;
using System.Linq;
using UploadPB.Services.Interfaces.IPostBC23Service;
using UploadPB.Services.Interfaces.IPostBC261Service;
using UploadPB.Services.Interfaces.IPostBC262Service;
using UploadPB.Services.Interfaces.IPostBC30Service;

namespace UploadPB.Controller.Post
{
    public class PostBeacukaiController : ControllerBase
    {
        public IPostBeacukai40 _postBeacukai40;
        public IPostBeacukai23 _postBeacukai23;
        public IPostBeacukai261 _postBeacukai261;
        public IPostBeacukai262 _postBeacukai262;
        public IPostBeacukai30 _postBeacukai30;

        public PostBeacukaiController(IPostBeacukai40 postBeacukai40, IPostBeacukai23 postBeacukai23, IPostBeacukai261 postBeacukai261, IPostBeacukai262 postBeacukai262, IPostBeacukai30 postBeacukai30)
        {
            _postBeacukai40 = postBeacukai40;
            _postBeacukai23 = postBeacukai23;
            _postBeacukai261 = postBeacukai261;
            _postBeacukai262 = postBeacukai262;
            _postBeacukai30 = postBeacukai30;
        }

        [FunctionName("PostBeacukaies")]
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
                foreach (var a in Data)
                {
                    if (a.Type == "40")
                    {
                        await _postBeacukai40.PostBeacukai(Data, Username);
                    }
                    else if (a.Type == "23")
                    {
                        await _postBeacukai23.PostBeacukai(Data, Username);
                    }
                    else if (a.Type == "261")
                    {
                        await _postBeacukai261.PostBeacukai(Data, Username);
                    }
                    else if (a.Type == "262")
                    {
                        await _postBeacukai262.PostBeacukai(Data, Username);
                    }
                    else if (a.Type == "30")
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
