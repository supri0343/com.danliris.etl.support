using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UploadPB.ViewModels;


namespace UploadPB.Services.Interfaces.IPostBC41Service
{
    public interface IUploadExcel41 : IBaseService
    {
        Task<int> Upload(ExcelWorksheets sheet);
    }
}
