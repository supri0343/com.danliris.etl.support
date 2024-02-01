using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using UploadPB.Services.Class;
using UploadPB.SupporttDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using UploadPB.Services.Interfaces.IPostBC261Service;
using UploadPB.Services.Class.PostBC261Service;
using UploadPB.SupporttDbContext.AG;
using UploadPB.Services.Interfaces.IPostBC30Service.PostAG;
using UploadPB.Services.Interfaces.IPostBC20Service.PostAG;

[assembly: FunctionsStartup(typeof(UploadPB.Startup))]
namespace UploadPB
{
    public class Startup : FunctionsStartup
    {
        //private readonly string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);

        public override void Configure(IFunctionsHostBuilder builder)
        {

            string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString");
            string AGDbConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings:AGConnectionString");
            builder.Services.AddDbContext<SupportDbContext>
                (options => SqlServerDbContextOptionsExtensions.UseSqlServer(options,connectionString));

            builder.Services.AddDbContext<AGDbContext>
              (options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, AGDbConnectionString));


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
                .AddTransient<Services.Interfaces.IPostBC261Service.IPostBeacukai261, Services.Class.PostBC261Service.PostBeacukaiService>()
                .AddTransient<Services.Interfaces.IPostBC262Service.IPostBeacukai262, Services.Class.PostBC262Service.PostBeacukaiService>()
                .AddTransient<Services.Interfaces.IPostBC30Service.IPostBeacukai30, Services.Class.PostBC30Service.PostBeacukaiService>()
                .AddTransient<Services.Interfaces.IPostBC27Service.IPostBeacukai27, Services.Class.PostBC27Service.PostBeacukaiService>()
                .AddTransient<Services.Interfaces.IPostBC30Service.PostAG.IPostBeacukai30, Services.Class.PostBC30Service.PostAG.PostBeacukaiService>()
                .AddTransient<IPostBeacukai20, Services.Class.PostBC20Service.PostAG.PostBeacukaiService>()
                .AddTransient<Services.Interfaces.IPostBC41Service.IPostBeacukai41, Services.Class.Post41.PostBeacukaiService>()

                .AddTransient<Services.Interfaces.IPostBC23.IUploadExcel, Services.Class.PostBC23.UploadExcelService>()
                .AddTransient<Services.Interfaces.IPostBC40Service.IUploadExcel40, Services.Class.PostBC40Service.UploadExcelService>()
                .AddTransient<Services.Interfaces.IPostBC261Service.IUploadExcel261, Services.Class.PostBC261Service.UploadExcelService>()
                .AddTransient<Services.Interfaces.IPostBC23Service.IUploadExcel23, Services.Class.PostBC23Service.UploadExcelService>()
                .AddTransient<Services.Interfaces.IPostBC262Service.IUploadExcel262, Services.Class.PostBC262Service.UploadExcelService>()
                .AddTransient<Services.Interfaces.IPostBC30Service.IUploadExcel30, Services.Class.PostBC30Service.UploadExcelService>()
                .AddTransient<Services.Interfaces.IPostBC27Service.IUploadExcel27, Services.Class.PostBC27Service.UploadExcelService>()
                .AddTransient<IUploadExcel30AG, Services.Class.PostBC30Service.PostAG.UploadExcelService>()
                .AddTransient<IUploadExcel20, Services.Class.PostBC20Service.PostAG.UploadExcelService>()
                .AddTransient<Services.Interfaces.IPostBC41Service.IUploadExcel41, Services.Class.PostBC41Service.UploadExcelService>();
        }
    }
}
