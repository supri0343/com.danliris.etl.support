using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using UploadPB.Services.Class;
using UploadPB.SupporttDbContext;
using Microsoft.EntityFrameworkCore;
using System;


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
                .AddTransient<Services.Interfaces.IPostBC23.IPostBeacukai, Services.Class.Post23.PostBeacukaiService>()
                .AddTransient<Services.Interfaces.Post40.IPostBeacukai40, Services.Class.Post40.PostBeacukaiService>()
                 .AddTransient<Services.Interfaces.IPostBC23Service.IPostBeacukai23, Services.Class.PostBC23Service.PostBeacukaiService>()
                .AddTransient<Services.Interfaces.IPostBC23.IUploadExcel, Services.Class.PostBC23.UploadExcelService>()
                .AddTransient<Services.Interfaces.IPostBC40Service.IUploadExcel40, Services.Class.PostBC40Service.UploadExcelService>()
                .AddTransient<Services.Interfaces.IPostBC23Service.IUploadExcel23, Services.Class.PostBC23Service.UploadExcelService>();
        }
    }
}
