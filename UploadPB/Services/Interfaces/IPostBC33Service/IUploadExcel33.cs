using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UploadPB.Services.Interfaces.IPostBC33Service
{
    public interface IUploadExcel33
    {
        Task<int> Upload(ExcelWorksheets sheet);
    }
}
