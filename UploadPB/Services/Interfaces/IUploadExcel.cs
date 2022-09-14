using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UploadPB.ViewModels;

namespace UploadPB.Services.Interfaces
{
    public interface IUploadExcel : IBaseService
    {
        Task<int> Upload(ExcelWorksheets sheet);
    }
}
