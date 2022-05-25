using UploadPB.Tools;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UploadPBTest.Tools
{
    public class ConverterCheckerTest
    {
        [Fact]
        public void ConvertDateTimeSuccess()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];
            var dateString = DateTime.Now.ToString("dd/MM/yyyy");
            var dateTime = DateTime.ParseExact(dateString, "dd/MM/yyyy", null);
            sheet.Cells[1, 1].Value = DateTime.Now.ToString();

            GeneralHelper generalHelper = new GeneralHelper();
            var result = generalHelper.ConvertDateTime(sheet.Cells[1, 1]);
            Assert.Equal(result, dateTime);
        }
        [Fact]
        public void ConvertNullDateTimeReturnSuccess()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];
            var dateString = DateTime.Now.ToString("dd/MM/yyyy");
            var dateTime = DateTime.ParseExact(dateString, "dd/MM/yyyy", null);
            sheet.Cells[1, 1].Value = DateTime.Now.ToString();

            GeneralHelper generalHelper = new GeneralHelper();
            var result = generalHelper.ConvertNullDateTime(sheet.Cells[1, 1]);
            Assert.Equal(result, dateTime);
        }
        [Fact]
        public void ConvertNullDateTimeReturnNull()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];
            sheet.Cells[1, 1].Value = string.Empty;

            GeneralHelper generalHelper = new GeneralHelper();
            var result = generalHelper.ConvertNullDateTime(sheet.Cells[1, 1]);            
            Assert.Null(result);
        }
        [Fact]
        public void GenerateIdSuccess()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;

            Random rnd = new Random();
            int no = rnd.Next(1, 10);
            var dateString = DateTime.Now.ToString("yyyyMM");

            excel.Add("Test");
            var sheet = excel[0];
            sheet.Cells[1, 1].Value = DateTime.Now.ToString();
            sheet.Cells[1, 2].Value = no.ToString();

            GeneralHelper generalHelper = new GeneralHelper();
            var result = generalHelper.GenerateId(sheet.Cells[1, 1], sheet.Cells[1, 2]);
            Assert.Equal(result, dateString + no.ToString("000"));
        }
        [Fact]
        public void ValidatePeriodeSuccess()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];
            sheet.Cells[1, 1].Value = DateTime.Now.ToString();

            GeneralHelper generalHelper = new GeneralHelper();
            generalHelper.ValidatePeriode(sheet.Cells[1, 1], DateTime.Now);
            Assert.True(true);
        }
        [Fact]
        public void ValidatePeriodeFailed()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];
            sheet.Cells[1, 1].Value = DateTime.Now.ToString();

            GeneralHelper generalHelper = new GeneralHelper();
            Exception message = Assert.ThrowsAny<Exception>(() => generalHelper.ValidatePeriode(sheet.Cells[1, 1], DateTime.Now.AddMonths(2)));
            Assert.Equal("Periode yang dipilih tidak sesuai dengan periode data excel", message.Message);
        }
    }
}
