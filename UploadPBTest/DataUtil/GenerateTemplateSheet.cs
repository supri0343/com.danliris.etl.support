using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;

namespace UploadPBTest.DataUtil
{
    public class GenerateTemplateSheet
    {
        public ExcelWorksheets GenerateTempalate(string name, int endRow, int endCol)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheets excel = excelPackage.Workbook.Worksheets;
            excel.Add(name);
            var sheet = excel[0];
            for (int row = 1; row < endRow; row++)
            {
                for (int col = 1; col <= endCol; col++)
                {
                    sheet.Cells[row, col].Value = name;
                }
            }

            return excel;
        }
    }
}
