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
using UploadPB.Models.Temporary;

namespace UploadPB.PostBC40
{
    public class GetTemporary
    {
  
        public IServiceProvider serviceProvider;
        private readonly SupportDbContext context;
        private readonly DbSet<Beacukai40Temporary> dbSet;

        public GetTemporary( SupportDbContext context)
        {
     
            this.context = context;
            this.dbSet = context.Set<Beacukai40Temporary>();
        }

        [FunctionName("GetTemporary40")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            var result = this.dbSet.Select(x => new 
            {
              
                NoAju = x.NoAju,
                BCNo = x.BCNo,
                TglBCNO = x.TglBCNO,
                NamaSupplier = x.NamaSupplier
            }).Distinct().ToList().OrderBy(x =>x.NoAju);

            if (result != null)
            {
                return new OkObjectResult(new ResponseSuccess("success", result));
            }

            return new OkObjectResult(new ResponseSuccess("Berhasil Menyimpan Data Temporary"));


        }
    }
}
