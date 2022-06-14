using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UploadPB.Models;
using UploadPB.ViewModels;
using UploadPB.Services.Interfaces;
using UploadPB.DBAdapters.BeacukaiTemp;


namespace UploadPB
{
    public class GetandPostTemporary
    {
        public IGetandPostTemporary _gettemp;
        public IServiceProvider serviceProvider;

        public GetandPostTemporary(IGetandPostTemporary gettempp)
        {
            _gettemp = gettempp;
        }

        [FunctionName("GetTemporary")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);

            var adapter = new BeacukaiTemp(connectionString);

            var result = await adapter.Get();

            if (adapter != null)
            {
                return new OkObjectResult(new ResponseSuccess("success", result));
            }

            return new OkObjectResult(new ResponseSuccess("Berhasil Menyimpan Data Temporary"));


        }
    }
}
