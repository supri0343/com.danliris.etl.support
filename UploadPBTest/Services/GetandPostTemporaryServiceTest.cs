//using Com.Danliris.ETL.Service.DBAdapters.DyeingAdapters;
//using Com.Danliris.ETL.Service.ExcelModels.DashboardDyeingModels;
using UploadPB.Services;
using UploadPB.Services.Class;
using UploadPBTest.DataUtil;
using System.Data;
using System.Data.SqlClient;
using UploadPBTest.Services;
using UploadPB.Tools;
using Microsoft.Extensions.Logging;
using Moq;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using UploadPB;
using UploadPB.Models;
using UploadPB.ViewModels;
using UploadPB.Models.BCTemp;
using UploadPB.DBAdapters;
using UploadPB.DBAdapters.GetTemporary;
using UploadPB.DBAdapters.Insert;


namespace UploadPBTest.Services
{
    public class GetandPostTemporaryServiceTest
    {
        GenerateTemplateSheet generateTemplateSheet = new GenerateTemplateSheet();
        
        UploadExcelServiceTest uploadExcelServiceTest = new UploadExcelServiceTest();

        //[Fact]
        //public async Task coba()
        //{
        //    var sqlDataContext = new Mock<ISqlDataContext<HeaderDokumenTempModel>>();
        //    var serviceProvider = new Mock<IServiceProvider>();

        //    sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<HeaderDokumenTempModel>())).ReturnsAsync(1);
        //    sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<HeaderDokumenTempModel>());
        //    serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<HeaderDokumenTempModel>))).Returns(sqlDataContext.Object);
        //    serviceProvider.Setup(x => x.GetService(typeof(IDokumenHeaderAdapter))).Returns(new DokumenHeaderAdapter(serviceProvider.Object));


        //    var sqlDataContext1 = new Mock<ISqlDataContext<HeaderDokumenTempViewModel>>();
        //    var serviceProvider1 = new Mock<IDbConnection>();

        //    sqlDataContext1.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<HeaderDokumenTempViewModel>())).ReturnsAsync(1);
        //    sqlDataContext1.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<HeaderDokumenTempViewModel>());
        //    //serviceProvider1.Setup(x => x.GetService(typeof(IDbConnection<HeaderDokumenTempViewModel>))).Returns(sqlDataContext1.Object);
        //    //serviceProvider1.Setup(x => x.GetService(typeof(IDbConnection))).Returns(new GetHeaderDokumen)
            
        //    UploadExcelService uploadExcelService = new UploadExcelService(serviceProvider.Object);
        //    GetandPostTemporaryService getandPostTemporaryService = new GetandPostTemporaryService(serviceProvider1.Object);

        //    var excel = uploadExcelServiceTest.GenerateHeaderDokumenSheet();
        //    await uploadExcelService.Upload(excel);
        //    await getandPostTemporaryService.CreateTemporary();
          
        //    Assert.True(true);
        //}

    }
}
