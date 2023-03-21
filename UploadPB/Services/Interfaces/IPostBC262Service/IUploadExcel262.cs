using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UploadPB.Services.Interfaces.IPostBC262Service
{
    public interface IUploadExcel262
    {
        Task<int> Upload(ExcelWorksheets sheet);
    }
}
