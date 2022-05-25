using UploadPB.Tools;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UploadPBTest.Tools
{
    public class ConverterCheckerTests
    {

        [Fact]
        public void GenerateValueStringSuccess()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);

            sheet.Cells[1, 1].Value = guid;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GenerateValueString(sheet.Cells[1, 1]);
            Assert.Equal(result, guid);
        }
        [Fact]
        public void GenerateValueStringReturnNull()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];

            sheet.Cells[1, 1].Value = null;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GenerateValueString(sheet.Cells[1, 1]);
            Assert.Null(result);
        }
        [Fact]
        public void GenerateValueIntSuccess()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];
            Random rnd = new Random();
            var no = rnd.Next(1, 10);
            sheet.Cells[1, 1].Value = no;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GenerateValueInt(sheet.Cells[1, 1]);
            Assert.Equal(result, no);
        }
        [Fact]
        public void GenerateValueIntReturnNull()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];

            sheet.Cells[1, 1].Value = null;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GenerateValueInt(sheet.Cells[1, 1]);
            Assert.Null(result);
        }
        [Fact]
        public void GenerateValueDoubleSuccess()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];
            Random rnd = new Random();
            var no = rnd.Next(1, 10);
            sheet.Cells[1, 1].Value = no;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GenerateValueDouble(sheet.Cells[1, 1]);
            Assert.Equal(result, no);
        }
        [Fact]
        public void GenerateValueDoubleReturnNull()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];

            sheet.Cells[1, 1].Value = null;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GenerateValueDouble(sheet.Cells[1, 1]);
            Assert.Null(result);
        }
        [Fact]
        public void GenerateValueDecimalSuccess()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];
            Random rnd = new Random();
            var no = rnd.Next(1, 10);
            sheet.Cells[1, 1].Value = no;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GenerateValueDecimal(sheet.Cells[1, 1]);
            Assert.Equal(result, no);
        }
        [Fact]
        public void GenerateValueDecimalReturnNull()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];

            sheet.Cells[1, 1].Value = null;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GenerateValueDecimal(sheet.Cells[1, 1]);
            Assert.Null(result);
        }
        [Fact]
        public void GenerateValueCharSuccess()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];
            var guid = Guid.NewGuid().ToString("N").Substring(0, 1);

            sheet.Cells[1, 1].Value = guid;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GenerateValueChar(sheet.Cells[1, 1]);
            Assert.Equal(result.ToString(), guid);
        }
        [Fact]
        public void GenerateValueCharReturnNull()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];

            sheet.Cells[1, 1].Value = null;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GenerateValueChar(sheet.Cells[1, 1]);
            Assert.Null(result);
        }
        [Fact]
        public void GeneratePureStringSuccess()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);

            sheet.Cells[1, 1].Value = guid;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GeneratePureString(sheet.Cells[1, 1]);
            Assert.Equal(result, guid);
        }
        [Fact]
        public void GeneratePureStringReturnNull()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];

            sheet.Cells[1, 1].Value = null;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GeneratePureString(sheet.Cells[1, 1]);
            Assert.Null(result);
        }
        [Fact]
        public void GenerateValueDateSuccess()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];
            var dateString = DateTime.Now.ToString("dd/MM/yyyy");
            var dateTime = DateTime.ParseExact(dateString, "dd/MM/yyyy", null);
            sheet.Cells[1, 1].Value = DateTime.Now.ToString();

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GenerateValueDate(sheet.Cells[1, 1]);
            Assert.Equal(result, dateTime);
        }
        [Fact]
        public void GenerateValueDateReturnNull()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];

            sheet.Cells[1, 1].Value = null;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GenerateValueDate(sheet.Cells[1, 1]);
            Assert.Null(result);
        }
        [Fact]
        public void GeneratePureDateTimeSuccess()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];
            var dateString = DateTime.Now.ToString("dd/MM/yyyy");
            var dateTime = DateTime.ParseExact(dateString, "dd/MM/yyyy", null);
            sheet.Cells[1, 1].Value = DateTime.Now.ToString();

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GeneratePureDateTime(sheet.Cells[1, 1]);
            Assert.Equal(result, dateTime);
        }
        [Fact]
        public void GeneratePureDateTimeReturnNull()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];

            sheet.Cells[1, 1].Value = null;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GeneratePureDateTime(sheet.Cells[1, 1]);
            Assert.Null(result);
        }
        [Fact]
        public void GeneratePureDoubleSuccess()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];
            Random rnd = new Random();
            var no = rnd.Next(1, 10);
            sheet.Cells[1, 1].Value = no;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GeneratePureDouble(sheet.Cells[1, 1]);
            Assert.Equal(result, no);
        }
        [Fact]
        public void GeneratePureDoubleReturnNull()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];

            sheet.Cells[1, 1].Value = null;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GeneratePureDouble(sheet.Cells[1, 1]);
            Assert.Null(result);
        }

        [Fact]
        public void GenerateValueStringBCSuccess()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            var gres = "BC " + guid;

            sheet.Cells[1, 1].Value = guid;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GenerateValueStringBC(sheet.Cells[1, 1]);
            Assert.Equal(result, gres);
        }
        [Fact]
        public void GenerateValueStringBCReturnNull()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add("Test");
            var sheet = excel[0];

            sheet.Cells[1, 1].Value = null;

            ConverterChecker converterChecker = new ConverterChecker();
            var result = converterChecker.GenerateValueStringBC(sheet.Cells[1, 1]);
            Assert.Null(result);
        }
    }
}
