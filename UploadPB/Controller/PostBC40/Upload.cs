using System;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using UploadPB.Models;
using UploadPB.Services.Interfaces.IPostBC40Service;
using UploadPB.Services.Class;
namespace UploadPB.Controller.PostBC40
{
    public class Upload
    {
        public IUploadExcel _uploadExcel;

        public Upload(IUploadExcel uploadExcelsService)
        {
            _uploadExcel = uploadExcelsService;
        }

        [FunctionName("UploadBC40")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

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

                        try
                        {
                            var data = await _uploadExcel.Upload(sheet);

                            return new OkObjectResult(new ResponseSuccess("success"));
                        }
                        catch (Exception ex)
                        {
                            return new BadRequestObjectResult(new ResponseFailed(ex.Message, ex.Data));
                        }
                    }
                }             
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new ResponseFailed("Gagal menyimpan data", ex));
            }

            return new OkObjectResult(new ResponseSuccess("success"));
        }
    }
}
