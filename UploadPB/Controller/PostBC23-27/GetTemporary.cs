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

namespace UploadPB
{
    public class GetTemporary
    {
  
        public IServiceProvider serviceProvider;
        private readonly SupportDbContext context;
        private readonly DbSet<BeacukaiTemporaryModel> dbSet;

        public GetTemporary( SupportDbContext context)
        {
     
            this.context = context;
            this.dbSet = context.Set<BeacukaiTemporaryModel>();
        }

        [FunctionName("GetTemporary")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            //var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);

            //var adapter = new BeacukaiTemp(connectionString);

            //var result = await adapter.Get();
            var result = this.dbSet.Select(x => new 
            {
                //ID = x.ID,
                NoAju = x.NoAju,
                BCNo = x.BCNo,
                TglBCNO = x.TglBCNO,
                NamaSupplier = x.NamaSupplier
            }).Distinct().ToList().OrderBy(x =>x.NoAju);

            //var results = result.Distinct();
            //var result = this.dbSet.Select(x => x).ToList();

            if (result != null)
            {
                return new OkObjectResult(new ResponseSuccess("success", result));
            }

            return new OkObjectResult(new ResponseSuccess("Berhasil Menyimpan Data Temporary"));


        }
    }
}
