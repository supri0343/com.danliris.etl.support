//using Com.Danliris.ETL.Service.DBAdapters.DyeingAdapters;
//using Com.Danliris.ETL.Service.ExcelModels.DashboardDyeingModels;
using UploadPB.Services;
using UploadPB.Services.Class;
using UploadPBTest.DataUtil;
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
using UploadPB.Models.BCTemp;
using UploadPB.DBAdapters;
using UploadPB.DBAdapters.Insert;

namespace UploadPBTest.Services
{
    public class UploadExcelServiceTest
    {
        GenerateTemplateSheet generateTemplateSheet = new GenerateTemplateSheet();

        public ExcelWorksheets GenerateHeaderDokumenSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 2;
            var excel = generateTemplateSheet.GenerateTempalate("HEADER DOKUMEN", startRow, 36);
            var sheet = excel[0];
            sheet.Cells[startRow, 1].Value = "ID";
            sheet.Cells[startRow, 3].Value = "BCNo-" + guid;
            sheet.Cells[startRow, 33].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 31].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 32].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 34].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 2].Value = "Aju-" + guid;
            sheet.Cells[startRow, 15].Value = "SupplierName-" + guid;
            sheet.Cells[startRow, 4].Value = DateTime.Now.ToString();
            sheet.Cells[startRow, 28].Value = "Val-" + guid;
            sheet.Cells[startRow, 6].Value = "BC " + guid;
            sheet.Cells[startRow, 35].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 14].Value = "KodeSUpplier-" + guid;

            return excel;

        }

        private ExcelWorksheets GenerateBarangSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 2;
            var excel = generateTemplateSheet.GenerateTempalate("Barang", startRow, 7);
            var sheet = excel[0];

            sheet.Cells[startRow, 2].Value = "Aju-" + guid;
            sheet.Cells[startRow, 5].Value = "Barang-" + guid;
            sheet.Cells[startRow, 9].Value = rnd.Next(1, 10);
            sheet.Cells[startRow, 8].Value = "KodeBarang-" + guid;
            sheet.Cells[startRow, 10].Value = "PCS";

            return excel;
        }

        private ExcelWorksheets GenerateDokumenPelengkapSheet()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Random rnd = new Random();

            var startRow = 2;
            var excel = generateTemplateSheet.GenerateTempalate("DOKUMEN PELENGKAP", startRow, 7);
            var sheet = excel[0];

            sheet.Cells[startRow, 2].Value = "Aju-" + guid;
            sheet.Cells[startRow, 3].Value = "INVOICE";
            sheet.Cells[startRow, 4].Value =  guid;
            sheet.Cells[startRow, 5].Value = DateTime.Now.ToString();

            return excel;
        }

        #region success
        [Fact]
        public async Task UploadHeaderDokumenSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<HeaderDokumenTempModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<HeaderDokumenTempModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<HeaderDokumenTempModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<HeaderDokumenTempModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDokumenHeaderAdapter))).Returns(new DokumenHeaderAdapter(serviceProvider.Object));

            UploadExcelService uploadExcelService = new UploadExcelService(serviceProvider.Object);

            var excel = GenerateHeaderDokumenSheet();
            await uploadExcelService.Upload(excel);
            Assert.True(true);
        }


        [Fact]
        public async Task UploadBarangSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<BarangTemp>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<BarangTemp>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<BarangTemp>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<BarangTemp>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IBarangAdapter))).Returns(new BarangAdapter(serviceProvider.Object));

            UploadExcelService uploadExcelService = new UploadExcelService(serviceProvider.Object);

            var excel = GenerateBarangSheet();
            await uploadExcelService.Upload(excel);
            Assert.True(true);
        }

        [Fact]
        public async Task UploadDokumenPelangkapSheetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DokumenPelengkapTemp>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DokumenPelengkapTemp>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DokumenPelengkapTemp>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DokumenPelengkapTemp>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDokumenPelengkapAdapter))).Returns(new DokumenPelengkapAdapter(serviceProvider.Object));

            UploadExcelService uploadExcelService = new UploadExcelService(serviceProvider.Object);

            var excel = GenerateDokumenPelengkapSheet();
            await uploadExcelService.Upload(excel);
            Assert.True(true);
        }

        #endregion

        #region failed
        [Fact]
        public async Task UploadHeaderDokumenSheetFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<HeaderDokumenTempModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<HeaderDokumenTempModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<HeaderDokumenTempModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<HeaderDokumenTempModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDokumenHeaderAdapter))).Returns(new DokumenHeaderAdapter(serviceProvider.Object));

            UploadExcelService uploadExcelService = new UploadExcelService(serviceProvider.Object);

            var excel = GenerateHeaderDokumenSheet();

            var sheet = excel[0];
            sheet.Cells[2, 33].Value = "BC-";

            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelService.Upload(excel));
            Assert.Equal("Gagal memproses Sheet Header Dokumen pada baris ke-2 - Input string was not in a correct format.", message.Message); ;
        }

        [Fact]
        public async Task UploadBarangSheetFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<BarangTemp>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<BarangTemp>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<BarangTemp>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<BarangTemp>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IBarangAdapter))).Returns(new BarangAdapter(serviceProvider.Object));

            UploadExcelService uploadExcelService = new UploadExcelService(serviceProvider.Object);

            var excel = GenerateBarangSheet();

            var sheet = excel[0];
            sheet.Cells[2, 9].Value = "BC-";

            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelService.Upload(excel));
            Assert.Equal("Gagal memproses Sheet Barang pada baris ke-2 - Input string was not in a correct format.", message.Message); ;

        }

        [Fact]
        public async Task UploadDokumenPelangkapSheetFailed()
        {
            var sqlDataContext = new Mock<ISqlDataContext<DokumenPelengkapTemp>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DokumenPelengkapTemp>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<DokumenPelengkapTemp>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<DokumenPelengkapTemp>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IDokumenPelengkapAdapter))).Returns(new DokumenPelengkapAdapter(serviceProvider.Object));

            UploadExcelService uploadExcelService = new UploadExcelService(serviceProvider.Object);

            var excel = GenerateDokumenPelengkapSheet();

            var sheet = excel[0];
            sheet.Cells[2, 5].Value = "BC-";

            Exception message = await Assert.ThrowsAnyAsync<Exception>(() => uploadExcelService.Upload(excel));
            Assert.Equal("Gagal memproses Sheet Dokumen Pelengkap pada baris ke-2 - The string 'BC-' was not recognized as a valid DateTime. There is an unknown word starting at index '0'.", message.Message); ;

        }
    
        #endregion

    }
}
