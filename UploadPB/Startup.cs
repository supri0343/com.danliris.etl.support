using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using UploadPB.Models;
using UploadPB.Models.BCTemp;
using UploadPB.Services;
using UploadPB.Services.Class;
using UploadPB.Services.Interfaces;
using UploadPB.DBAdapters;
using UploadPB.DBAdapters.Insert;
using UploadPB.DBAdapters.BeacukaiTemp;
//using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(UploadPB.Startup))]
namespace UploadPB
{
    public class Startup : FunctionsStartup
    {
        private readonly string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddSingleton<ISqlDataContext<HeaderDokumenTempModel>>((s) =>
                {
                    return new SqlDataContext<HeaderDokumenTempModel>(connectionString);
                })
                .AddSingleton<ISqlDataContext<BarangTemp>>((s) =>
                {
                    return new SqlDataContext<BarangTemp>(connectionString);
                })
                .AddSingleton<ISqlDataContext<DokumenPelengkapTemp>>((s) =>
                {
                    return new SqlDataContext<DokumenPelengkapTemp>(connectionString);
                })
                .AddSingleton<ISqlDataContext<TemporaryModel>>((s) =>
                {
                    return new SqlDataContext<TemporaryModel>(connectionString);
                });



            builder.Services
                .AddTransient<IDokumenHeaderAdapter, DokumenHeaderAdapter>()
                .AddTransient<IBarangAdapter, BarangAdapter>()
                .AddTransient<IDokumenPelengkapAdapter, DokumenPelengkapAdapter>()
                .AddTransient<IUploadExcel, UploadExcelService>();

            builder.Services
                .AddTransient<IBeacukaiTemp, BeacukaiTemp>()
                .AddTransient<IGetandPostTemporary, GetandPostTemporaryService>();

            builder.Services
                .AddTransient<IPostBeacukai, PostBeacukaiService>();
        }
    }
}
