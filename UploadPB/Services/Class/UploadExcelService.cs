using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using System.Linq;
using System.Threading.Tasks;
using UploadPB.Services.Interfaces;
using UploadPB.Models;
using UploadPB.Models.BCTemp;
using UploadPB.Tools;
using UploadPB.DBAdapters;
using UploadPB.DBAdapters.Insert;

//using Com.Danliris.ETL.Service.DBAdapters.DyeingAdapters;
//using Com.Danliris.ETL.Service.ExcelModels.DashboardDyeingModels;

namespace UploadPB.Services.Class
{
    public class UploadExcelService : IUploadExcel
    {
        static string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);
        ConverterChecker converterChecker = new ConverterChecker();
        public IDokumenHeaderAdapter headerAdapter;
        public IBarangAdapter barangAdapter;
        public IDokumenPelengkapAdapter dokumenPelengkapAdapter;
        
        public UploadExcelService(IServiceProvider provider)
        {
            headerAdapter = provider.GetService<IDokumenHeaderAdapter>();
            barangAdapter = provider.GetService<IBarangAdapter>();
            dokumenPelengkapAdapter = provider.GetService<IDokumenPelengkapAdapter>();
        }

        public async Task Upload(ExcelWorksheets sheet)
        {
            try
            {
                var data = 0;
                //foreach (var item in sheet)
                //{
                //    switch (item.Name.ToUpper())
                //    {
                //        case "HEADER DOKUMEN":
                //            await UploadHeader(sheet, data);
                //            break;
                //        case "BARANG":
                //            await UploadBarang(sheet, data);
                //            break;
                //        case "DOKUMEN PELENGKAP":
                //            await UploadDokumenPelengkap(sheet, data);
                //            break;




                //        default:
                //            throw new Exception(item.Name + " tidak valid");
                //    }
                //    data++;
                //}

                var count = sheet.Count();
                for (var i = 0; i < count; i++  )
                {
                    if(sheet[i].Name.ToUpper() == "HEADER DOKUMEN")
                    {
                        await UploadHeader(sheet, data);
                    }
                    if (sheet[i].Name.ToUpper() == "BARANG")
                    {
                        await UploadBarang(sheet, data);
                    }
                    if (sheet[i].Name.ToUpper() == "DOKUMEN PELENGKAP")
                    {
                        await UploadDokumenPelengkap(sheet, data);
                    }

                    data++;
                }
            } catch (Exception ex)
            {
                throw ex;
            }
        }


        private async Task UploadDokumenPelengkap(ExcelWorksheets excel, int data)
        {
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DokumenPelengkapTemp>();
            int rowIndex = 0;
            try{
                for (rowIndex = 2; rowIndex <= totalRow; rowIndex++){
                    if (sheet.Cells[rowIndex, 2].Value != null)
                    {
                        listData.Add(new DokumenPelengkapTemp(
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 2]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 3]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 4]),
                              converterChecker.GenerateValueDate(sheet.Cells[rowIndex, 5])
                      
                            ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet Dokumen Pelengkap pada baris ke-{rowIndex} - {ex.Message}");
            }

            try
            {
                if (listData.Count() > 0)
                {
                    await dokumenPelengkapAdapter.DeleteBulk();
                    await dokumenPelengkapAdapter.Insert(listData);
                 
                }
            }catch (Exception ex)
            {
                throw new Exception($"Gagal menyimpan Sheet Dokumen Pelengkap - " + ex.Message);
            }

        }


        private async Task UploadHeader(ExcelWorksheets excel, int data)
        {
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<HeaderDokumenTempModel>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = 2; rowIndex <= totalRow; rowIndex++)
                {
                    if (sheet.Cells[rowIndex, 3].Value != null)
                    {
                        listData.Add(new HeaderDokumenTempModel(
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex, 3]),
                            converterChecker.GenerateValueDouble(sheet.Cells[rowIndex, 33]),
                            converterChecker.GenerateValueDouble(sheet.Cells[rowIndex, 31]),
                            converterChecker.GenerateValueDouble(sheet.Cells[rowIndex, 32]),
                            converterChecker.GenerateValueDouble(sheet.Cells[rowIndex, 34]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex, 2]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex, 15]),
                            converterChecker.GenerateValueDate(sheet.Cells[rowIndex, 4]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex, 28]),
                            converterChecker.GenerateValueStringBC(sheet.Cells[rowIndex, 6]),
                            converterChecker.GenerateValueDouble(sheet.Cells[rowIndex, 35]),
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex, 14])
                            ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet Header Dokumen pada baris ke-{rowIndex} - {ex.Message}");
            }
            try
            {
                if (listData.Count() > 0)
                {
                    await headerAdapter.DeleteBulk();
                    await headerAdapter.Insert(listData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal menyimpan Sheet Header Dokumen - " + ex.Message);
            }
        }

        private async Task UploadBarang(ExcelWorksheets excel, int data)
        {
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<BarangTemp>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = 2; rowIndex <= totalRow; rowIndex++)
                {
                    if (sheet.Cells[rowIndex, 2].Value != null)
                    {
                        listData.Add(new BarangTemp(
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 2]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 5]),
                              converterChecker.GenerateValueDouble(sheet.Cells[rowIndex, 9]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 8]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 10])
                            ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet Barang pada baris ke-{rowIndex} - {ex.Message}");
            }
            try
            {
                if (listData.Count() > 0)
                {
                    await barangAdapter.DeleteBulk();
                    await barangAdapter.Insert(listData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal menyimpan Sheet Barang - " + ex.Message);
            }

        }
    }
}

