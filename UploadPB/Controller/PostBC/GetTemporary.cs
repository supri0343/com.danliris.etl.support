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
using System.Collections.Generic;
using UploadPB.ViewModels;

namespace UploadPB.PostBC
{
    public class GetTemporary
    {
  
        public IServiceProvider serviceProvider;
        private readonly SupportDbContext context;
        private readonly DbSet<Beacukai40Temporary> dbSet40;
        private readonly DbSet<Beacukai261Temporary> dbSet261;
        private readonly DbSet<Beacukai23Temporary> dbSet23;
        private readonly DbSet<Beacukai262Temporary> dbSet262;
        private readonly DbSet<Beacukai30HeaderTemporary> dbSet30;
        private readonly DbSet<Beacukai27Temporary> dbSet27;
        private readonly DbSet<Beacukai41Temporary> dbSet41;
        private readonly DbSet<Beacukai25Temporary> dbSet25;
        public GetTemporary( SupportDbContext context)
        {
     
            this.context = context;
            this.dbSet40 = context.Set<Beacukai40Temporary>();
            this.dbSet23 = context.Set<Beacukai23Temporary>();
            this.dbSet261 = context.Set<Beacukai261Temporary>();
            this.dbSet262 = context.Set<Beacukai262Temporary>();
            this.dbSet30 = context.Set<Beacukai30HeaderTemporary>();
            this.dbSet27 = context.Set<Beacukai27Temporary>();
            this.dbSet41 = context.Set<Beacukai41Temporary>();
            this.dbSet25 = context.Set<Beacukai25Temporary>();
        }

        [FunctionName("GetTemporarys")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            req.Query.TryGetValue("type", out Microsoft.Extensions.Primitives.StringValues type);

            var result = new List<TemporaryToViewModel>();
            if (type == "40")
            {
                result = this.dbSet40.Select(x => new TemporaryToViewModel
                {
                    NoAju = x.NoAju,
                    BCNo = x.BCNo,
                    BCType = x.JenisBC,
                    TglBCNO = x.TglBCNO,
                    NamaSupplier = x.NamaSupplier
                }).Distinct().OrderBy(x => x.NoAju).ToList();
            }
            else if (type == "23")
            {
                result = this.dbSet23.Select(x => new TemporaryToViewModel
                {
                    NoAju = x.NoAju,
                    BCNo = x.BCNo,
                    BCType = x.JenisBC,
                    TglBCNO = x.TglBCNO,
                    NamaSupplier = x.NamaSupplier
                }).Distinct().OrderBy(x => x.NoAju).ToList();
            }
            else if (type == "261")
            {
                result = this.dbSet261.Select(x => new TemporaryToViewModel
                {
                    NoAju = x.NoAju,
                    BCNo = x.BCNo,
                    BCType = x.JenisBC,
                    TglBCNO = x.TglBCNO,
                    NamaSupplier = x.NamaSupplier
                }).Distinct().OrderBy(x => x.NoAju).ToList();
            }
            else if (type == "262")
            {
                result = this.dbSet262.Select(x => new TemporaryToViewModel
                {
                    NoAju = x.NoAju,
                    BCNo = x.BCNo,
                    BCType = x.JenisBC,
                    TglBCNO = x.TglBCNO,
                    NamaSupplier = x.NamaSupplier
                }).Distinct().OrderBy(x => x.NoAju).ToList();
            }
            else if (type == "30")
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
            else if (type == "27")
            {
                result = this.dbSet27.Select(x => new TemporaryToViewModel
                {
                    NoAju = x.NoAju,
                    BCNo = x.BCNo,
                    BCType = x.JenisBC,
                    TglBCNO = x.TglBCNO,
                    NamaSupplier = x.NamaSupplier
                }).Distinct().OrderBy(x => x.NoAju).ToList();
            }
            else if (type == "41")
            {
                result = this.dbSet41.Select(x => new TemporaryToViewModel
                {
                    NoAju = x.NoAju,
                    BCNo = x.BCNo,
                    BCType = x.JenisBC,
                    TglBCNO = x.TglBCNO,
                    NamaSupplier = x.NamaSupplier
                }).Distinct().OrderBy(x => x.NoAju).ToList();
            } else if (type == "25")
            {
                result = this.dbSet25.Select(x => new TemporaryToViewModel
                {
                    NoAju = x.NoAju,
                    BCNo = x.BCNo,
                    BCType = x.JenisBC,
                    TglBCNO = x.TglBCNO,
                    NamaSupplier = x.NamaSupplier
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
