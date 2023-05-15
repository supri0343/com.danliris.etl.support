using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UploadPB.Services.Interfaces.IPostBC30Service
{
    public interface IUploadExcel30
    {
        Task<int> Upload(ExcelWorksheets sheet);
    }
}
