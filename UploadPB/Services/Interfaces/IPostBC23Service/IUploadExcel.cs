using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UploadPB.ViewModels;
using UploadPB.Services.Class.Post40;

namespace UploadPB.Services.Interfaces.IPostBC23Service
{
    public interface IUploadExcel23 : IBaseService
    {
        Task<int> Upload(ExcelWorksheets sheet);
    }
}
