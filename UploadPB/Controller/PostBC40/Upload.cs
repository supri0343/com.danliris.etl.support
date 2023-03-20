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
using UploadPB.Services.Interfaces.IPostBC23Service;
using UploadPB.Tools;

namespace UploadPB.Controller.PostBC40
{
    public class Upload
    {
        public IUploadExcel40 _uploadExcel40;
        public IUploadExcel23 _uploadExcel23;
        ConverterChecker converterChecker = new ConverterChecker();

        public Upload(IUploadExcel40 uploadExcel40, IUploadExcel23 uploadExcel23)
        {
            _uploadExcel40 = uploadExcel40;
            _uploadExcel23 = uploadExcel23;
        }

        [FunctionName("UploadBC")]
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
                req.Form.TryGetValue("type", out Microsoft.Extensions.Primitives.StringValues type);


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
                        var sheetData = sheet[0];
                        var typeFromSheet = converterChecker.GenerateValueString(sheetData.Cells[2, 2]);
                        try
                        {
                            if (type == "40" && typeFromSheet == "40")
                            {
                                var data = await _uploadExcel40.Upload(sheet);
                            }
                            else if (type == "23" && typeFromSheet == "23")
                            {
                                var data = await _uploadExcel23.Upload(sheet);
                            }

                            else if (type != typeFromSheet)
                            {
                                throw new Exception("Harap Upload File Sesuai Dengan Tipe BC yang Dipilih");
                            }
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
