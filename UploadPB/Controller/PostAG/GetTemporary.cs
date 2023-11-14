using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using UploadPB.Models;
using UploadPB.SupporttDbContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UploadPB.ViewModels;
using UploadPB.SupporttDbContext.AG;
using UploadPB.Models.Temporary.AGSupport;

namespace UploadPB.PostBC.PostAG
{
    public class GetTemporary
    {
  
        public IServiceProvider serviceProvider;
        private readonly AGDbContext context;

        private readonly DbSet<Beacukai30HeaderTemporary> dbSet30;

        public GetTemporary(AGDbContext context)
        {
     
            this.context = context;

            this.dbSet30 = context.Set<Beacukai30HeaderTemporary>();
        }

        [FunctionName("GetTemporarys-AG")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            req.Query.TryGetValue("type", out Microsoft.Extensions.Primitives.StringValues type);

            var result = new List<TemporaryToViewModel>();
           
            if (type == "30")
            {
                result = this.dbSet30.Select(x => new TemporaryToViewModel
                {
                    NoAju = x.CAR,
                    BCNo = x.BCNo,
                    BCType = x.BCType,
                    TglBCNO = x.BCDate,
                    NamaSupplier = x.BuyerName
                }).Distinct().OrderBy(x => x.NoAju).ToList();
            }


            if (result != null)
            {
                return new OkObjectResult(new ResponseSuccess("success", result));
            }

            return new OkObjectResult(new ResponseSuccess("Berhasil Menyimpan Data Temporary"));


        }
    }
}
