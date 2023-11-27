using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using UploadPB.Models;
using UploadPB.Services.Interfaces.IPostBC23;
using UploadPB.Services.Class;
using System.Collections.Generic;

namespace UploadPB
{
    public class Upload
    {
        public IUploadExcel _uploadExcel;
        public IServiceProvider serviceProvider;
        public IdentityService identityService;

        public Upload(IUploadExcel uploadExcelsService, IServiceProvider serviceProvider)
        {
            _uploadExcel = uploadExcelsService;
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


            List<string> EXTENSION = new List<string>(new string[] { ".xlsx", ".XLSX" });

            try
            {
                var formdata = await req.ReadFormAsync();
                IFormFile file = req.Form.Files["file"];
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                if (EXTENSION.Contains(System.IO.Path.GetExtension(file.FileName)))
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
                            //await adapter.DeleteBulk();
                            //await adapter.Insert(data);

                            ////var CreateTemporaray = await _uploadExcel.InsertToTemporary(data);

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
