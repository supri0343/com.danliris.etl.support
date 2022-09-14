using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UploadPB.ViewModels;

namespace UploadPB.Services.Interfaces
{
    public interface IBaseService
    {
        //Task Upload(ExcelWorksheets excel);
        Task<int> Upload(ExcelWorksheets sheet);
    }
}
