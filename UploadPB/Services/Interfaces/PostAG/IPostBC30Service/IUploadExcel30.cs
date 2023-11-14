using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UploadPB.Services.Interfaces.IPostBC30Service.PostAG
{
    public interface IUploadExcel30AG
    {
        Task<int> Upload(ExcelWorksheets sheet);
    }
}
