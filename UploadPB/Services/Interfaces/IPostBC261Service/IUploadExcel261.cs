using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UploadPB.Services.Interfaces.IPostBC261Service
{
    public interface IUploadExcel261
    {
        Task<int> Upload(ExcelWorksheets sheet);
    }
}
