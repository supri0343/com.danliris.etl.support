using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System.Collections.Generic;
//using Com.Danliris.ETL.Service.ExcelModels;
using UploadPB.Models;
using UploadPB.DBAdapters.BeacukaiTemp;
using UploadPB.Services.Interfaces;
using UploadPB.Services.Class;
using UploadPB.DBAdapters;
//using Com.Danliris.ETL.Service.Services.Class;
//using Com.Danliris.ETL.Service.DBAdapters;
using System.Linq;
//using Com.Danliris.ETL.Service.Services.Interfaces;


namespace UploadPB
{
    public class Upload
    {
        public IUploadExcel _uploadExcel;
        public IGetandPostTemporary _getandPostTemporary;
        public IServiceProvider serviceProvider;
        public IdentityService identityService;

        public Upload(IUploadExcel uploadExcelsService, IGetandPostTemporary getandPostTemporary, IServiceProvider serviceProvider)
        {
            _uploadExcel = uploadExcelsService;
            _getandPostTemporary = getandPostTemporary;
            identityService = (IdentityService)serviceProvider.GetService(typeof(IdentityService));

        }

        [FunctionName("UploadPB")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);
            var adapter = new BeacukaiTemp(connectionString);

            //identityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;


            const string EXTENSION = ".xlsx";

            try
            {
                var formdata = await req.ReadFormAsync();
                IFormFile file = req.Form.Files["file"];
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                if (System.IO.Path.GetExtension(file.FileName) == EXTENSION)
                {

                    using (var excelPack = new ExcelPackage())
                    {
                        using (var stream = file.OpenReadStream())
                        {
                            excelPack.Load(stream);

                        }
                        var sheet = excelPack.Workbook.Worksheets;

                        //var uploadExcel = new UploadExcelService(serviceProvider);
                       
                        try
                        {


                            //await adapter.DeleteDokumentTemp();
                            //await adapter.DeleteBarangTemp();
                            //await adapter.DeleteDokumentPelengTemp();

                            var data = await _uploadExcel.Upload(sheet);
                            //await _getandPostTemporary.CreateTemporary();
                            await adapter.DeleteBulk();
                            await adapter.Insert(data);

                            return new OkObjectResult(new ResponseSuccess("success"));

                        }
                        catch (Exception ex)
                        {
                            return new BadRequestObjectResult(new ResponseFailed(ex.Message, ex.Data));
                        }
                    }
                }
                //await _getandPostTemporary.CreateTemporary();               
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new ResponseFailed("Gagal menyimpan data", ex));
            }

            //var result = await adapter.Get();

            return new OkObjectResult(new ResponseSuccess("success"));
        }
    }
}
