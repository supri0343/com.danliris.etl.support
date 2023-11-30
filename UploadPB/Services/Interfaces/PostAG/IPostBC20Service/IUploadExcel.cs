using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UploadPB.ViewModels;
using UploadPB.Services.Class.Post40;

namespace UploadPB.Services.Interfaces.IPostBC20Service.PostAG
{
    public interface IUploadExcel20 : IBaseService
    {
        Task<int> Upload(ExcelWorksheets sheet);
    }
}
