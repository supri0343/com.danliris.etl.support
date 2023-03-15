using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using UploadPB.Models;
using UploadPB.Models.BCTemp;
using UploadPB.Services;
using UploadPB.Services.Class;
using UploadPB.Services.Interfaces.IPostBC23;
using UploadPB.Services.Interfaces.IPostBC40Service;
using UploadPB.DBAdapters;
using UploadPB.DBAdapters.Insert;
using UploadPB.DBAdapters.BeacukaiTemp;
using UploadPB.SupporttDbContext;
//using Microsoft.Extensions.Http;
using Microsoft.Azure.Functions.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UploadPB.Services.Class.IPostBC23;
using UploadPB.Services.Class.PostBC40Service;

[assembly: FunctionsStartup(typeof(UploadPB.Startup))]
namespace UploadPB
{
    public class Startup : FunctionsStartup
    {
        //private readonly string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);

        public override void Configure(IFunctionsHostBuilder builder)
        {

            string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString");
            builder.Services.AddDbContext<SupportDbContext>
                (options => SqlServerDbContextOptionsExtensions.UseSqlServer(options,connectionString));


            //builder.Services
            //    .AddSingleton<ISqlDataContext<HeaderDokumenTempModel>>((s) =>
            //    {
            //        return new SqlDataContext<HeaderDokumenTempModel>(connectionString);
            //    })
            //    .AddSingleton<ISqlDataContext<BarangTemp>>((s) =>
            //    {
            //        return new SqlDataContext<BarangTemp>(connectionString);
            //    })
            //    .AddSingleton<ISqlDataContext<DokumenPelengkapTemp>>((s) =>
            //    {
            //        return new SqlDataContext<DokumenPelengkapTemp>(connectionString);
            //    })
            //    .AddSingleton<ISqlDataContext<TemporaryModel>>((s) =>
            //    {
            //        return new SqlDataContext<TemporaryModel>(connectionString);
            //    });

            builder.Services.AddScoped<IdentityService>();

            //builder.Services
            //    .AddTransient<IDokumenHeaderAdapter, DokumenHeaderAdapter>()
            //    .AddTransient<IBarangAdapter, BarangAdapter>()
            //    .AddTransient<IDokumenPelengkapAdapter, DokumenPelengkapAdapter>()
                

            //builder.Services
            //    .AddTransient<IBeacukaiTemp, BeacukaiTemp>();
         

            builder.Services
                .AddTransient<Services.Interfaces.IPostBC23.IPostBeacukai, PostBeacukaiService>()
                .AddTransient<Services.Interfaces.IPostBC23.IUploadExcel, Services.Class.IPostBC23.UploadExcelService>()
                .AddTransient<Services.Interfaces.IPostBC40Service.IUploadExcel, Services.Class.PostBC40Service.UploadExcelService>();
        }
    }
}
