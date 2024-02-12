using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using System.Linq;
using System.Threading.Tasks;
using UploadPB.Tools;
using UploadPB.DBAdapters;
using UploadPB.DBAdapters.Insert;
using UploadPB.ViewModels;
using Microsoft.EntityFrameworkCore;
using UploadPB.Services.Interfaces.IPostBC30Service.PostAG;
using UploadPB.SupporttDbContext.AG;
using UploadPB.Models.Temporary.AGSupport;
using UploadPB.Models.AGSupport;
using UploadPB.Models;
using UploadPB.Models.BCTemp;
using BeacukaiDocumentsModel = UploadPB.Models.AGSupport.BeacukaiDocumentsModel;

namespace UploadPB.Services.Class.PostBC30Service.PostAG
{
    public class UploadExcelService : IUploadExcel30AG
    {
        private readonly AGDbContext context;
        private readonly DbSet<Beacukai30HeaderTemporary> dbSet;
        private readonly DbSet<BeacukaiDocumentsModel> beacukaiDocuments;

        ConverterChecker converterChecker = new ConverterChecker();
        
        public UploadExcelService(IServiceProvider provider, AGDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Beacukai30HeaderTemporary>();
            this.beacukaiDocuments = context.Set<BeacukaiDocumentsModel>();
        }

        public async Task<int> Upload(ExcelWorksheets sheet)
        {
            int Upload = 0;

            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    var data = 0;

                    var ListHeader = new List<HeaderDokumenTempModel30>();
                    var ListBarang = new List<BarangTemp>();
                    var ListDokument = new List<DokumenPelengkapTemp>();
                    var ListEntitas= new List<EntitasTemp>();
                    var ListKemasan = new List<KemasanTemp>();

                    var count = sheet.Count();
                    for (var i = 0; i < count; i++)
                    {
                        if (sheet[i].Name.ToUpper() == "HEADER")
                        {
                            ListHeader = UploadHeader(sheet, data);
                        }
                        if (sheet[i].Name.ToUpper() == "BARANG")
                        {
                            ListBarang = UploadBarang(sheet, data);
                        }
                        if (sheet[i].Name.ToUpper() == "DOKUMEN")
                        {
                            ListDokument = UploadDokumenPelengkap(sheet, data);
                        }
                        if (sheet[i].Name.ToUpper() == "ENTITAS")
                        {
                            ListEntitas = UploadEntitas(sheet, data);
                        }
                        if (sheet[i].Name.ToUpper() == "KEMASAN")
                        {
                            ListKemasan = UploadKemasan(sheet, data);
                        }
                        data++;
                    }

                    foreach(var a in ListHeader)
                    {
                        var DataEntitas = ListEntitas.Where(x => x.NoAju == a.NoAju).First();

                        a.NamaSupplier = DataEntitas.NamaSupplier;
                        a.KodeSupplier = DataEntitas.KodeSupplier;
                        a.Vendor = DataEntitas.Vendor;
                    }

                    var queryitem = (from a in ListHeader
                                     join b in ListBarang on a.NoAju equals b.NoAju
                                     select new Beacukai30ItemsTemporary
                                     {
                                         BCId="",
                                         DetailBCId = "",
                                         CAR = a.NoAju,
                                         ItemCode = b.KodeBarang,
                                         ItemName = b.Barang,
                                         UnitQtyCode = "-",
                                         Quantity = Convert.ToDouble(b.JumlahSatBarang),
                                         Price = (decimal)b.CIF_Rupiah,
                                         CurrencyCode = a.Valuta,
                                         UomUnit = b.Sat,
                                     }).ToList();

                    var queryheader = (from a in ListHeader
                                       join b in ListKemasan on a.NoAju equals b.NoAju
                                       join c in ListEntitas on a.NoAju equals c.NoAju
                                       join d in ListDokument on a.NoAju equals d.NoAju
                                       select new Beacukai30HeaderTemporary
                                       {
                                           BCId="",
                                           BCType = "BC 3.0",
                                           BCNo = a.BCNo,
                                           CAR = a.NoAju,
                                           BCDate = a.TglBCNO.Value.DateTime,
                                           ExpenditureNo = d.NomorDokumen,
                                           ExpenditureDate = d.TanggalDokumen.Value.DateTime,
                                           BuyerCode = "-",
                                           BuyerName = a.NamaSupplier,
                                           Vendor = a.NamaSupplier,
                                           Netto = Convert.ToDouble(a.Netto),
                                           Bruto = Convert.ToDouble(a.Bruto),
                                           Pack = b.KodeKemasan,
                                           Country = a.Country
                                           //Items = queryitem
                                       }).ToList();





                    //delete all temporaray data
                    var dataHeader = context.Set<Beacukai30HeaderTemporary>();
                    context.beacukai30HeaderTemporaries.RemoveRange(dataHeader);
                    context.SaveChanges();

                    var itemtoDelete = context.Set<Beacukai30ItemsTemporary>();
                    context.Beacukai30ItemsTemporaries.RemoveRange(itemtoDelete);
                    context.SaveChanges();

                    transaction.Commit();

                //UploadTemporaraydData
                Upload = await InsertToTemporary(queryheader,queryitem);

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
        }
            return Upload;
        }


        public async Task<int> InsertToTemporary(List<Beacukai30HeaderTemporary> data, List<Beacukai30ItemsTemporary> items)
        {
            int Created = 0;
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    long index = 1;
                    foreach(var a in data)
                    {
                        a.BCId = index.ToString();
                  
                        index++;
                        this.dbSet.Add(a);
                    }

                    long indexItem = 1;
                    foreach (var item in items)
                    {
                        item.DetailBCId = index.ToString() + indexItem.ToString();
                        indexItem++;
                        context.Beacukai30ItemsTemporaries.Add(item);
                    }

                    Created = await context.SaveChangesAsync();
                    transaction.Commit();

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return Created;
        }

        public List<EntitasTemp> UploadEntitas(ExcelWorksheets excel, int data)
        {
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<EntitasTemp>();
            var Vendor = "";
            int rowIndex = 0;
            try
            {
               
                for (rowIndex = 2; rowIndex <= totalRow; rowIndex++)
                {
                    var DataEntitas = new EntitasTemp("","","","");
                    if (sheet.Cells[rowIndex, 1].Value != null && converterChecker.GenerateValueInt(sheet.Cells[rowIndex, 3]) == 6) 
                    {
                        DataEntitas.NamaSupplier = converterChecker.GenerateValueString(sheet.Cells[rowIndex, 6]);
                        DataEntitas.KodeSupplier = converterChecker.GenerateValueString(sheet.Cells[rowIndex, 5]);
                        DataEntitas.NoAju = converterChecker.GenerateValueString(sheet.Cells[rowIndex, 1]);
                        DataEntitas.Vendor = converterChecker.GenerateValueString(sheet.Cells[rowIndex, 6]);

                    }
                    //if (sheet.Cells[rowIndex, 1].Value != null && converterChecker.GenerateValueInt(sheet.Cells[rowIndex, 3]) == 2)
                    //{
                    //    Vendor = converterChecker.GenerateValueString(sheet.Cells[rowIndex, 6]);
                    //}
                    listData.Add(DataEntitas);
                }

                //foreach (var a in listData)
                //{
                //    a.Vendor = Vendor;
                //}


                return listData;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet Entitas pada baris ke-{rowIndex} - {ex.Message}");
            }
        }

        public List<DokumenPelengkapTemp> UploadDokumenPelengkap(ExcelWorksheets excel, int data)
        {
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<DokumenPelengkapTemp>();
            int rowIndex = 0;
            try{
                for (rowIndex = 2; rowIndex <= totalRow; rowIndex++){
                    if (sheet.Cells[rowIndex, 2].Value != null && converterChecker.GenerateValueInt(sheet.Cells[rowIndex, 3]) == 380)
                    {
                        listData.Add(new DokumenPelengkapTemp(
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 1]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 3]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 4]),
                              converterChecker.GenerateValueDate(sheet.Cells[rowIndex, 5])
                            ));
                    }
                }

                return listData;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet Dokumen Pelengkap pada baris ke-{rowIndex} - {ex.Message}");
            }
        }


        public List<HeaderDokumenTempModel30> UploadHeader(ExcelWorksheets excel, int data)
        {
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<HeaderDokumenTempModel30>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = 2; rowIndex <= totalRow; rowIndex++)
                {
                    if (sheet.Cells[rowIndex, 94].Value != null)
                    {
                        listData.Add(new HeaderDokumenTempModel30(
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex, 94]),//bcNo
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex, 80]),//bruto
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex, 73]),//CIF
                            null,//CIF_Rupiah
                            converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex, 81]),//Netto
                            converterChecker.GenerateValueString(sheet.Cells[rowIndex, 1]),//NoAju
                            "",//NamaSupplier
                            converterChecker.GeneratePureDateTime(sheet.Cells[rowIndex, 95]),//TglBCNO
                             converterChecker.GenerateValueString(sheet.Cells[rowIndex, 87]),//Valuta
                            converterChecker.GenerateValueStringBC(sheet.Cells[rowIndex, 2]),//JenisBC
                            0,//JumlahBarang
                            "",//KodeSupplier
                             "",
                             converterChecker.GenerateValueString(sheet.Cells[rowIndex, 34])//Country but in Vendor.
                            ));
                    }
                }
                return listData;
            }


            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet Header Dokumen pada baris ke-{rowIndex} - {ex.Message}");
            }
           
            
        }

        public List<BarangTemp> UploadBarang(ExcelWorksheets excel, int data)
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
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 1]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 5]),
                              converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex, 11]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 4]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 10]),
                              converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex, 29]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 12]),
                              0
                            ));
                    }
                }
                return listData;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet Barang pada baris ke-{rowIndex} - {ex.Message}");
            }
        }

        public List<KemasanTemp> UploadKemasan(ExcelWorksheets excel, int data)
        {
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<KemasanTemp>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = 2; rowIndex <= totalRow; rowIndex++)
                {
                    if (sheet.Cells[rowIndex, 2].Value != null )
                    {
                        listData.Add(new KemasanTemp(
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 1]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 3])
                            ));
                    }
                }

                return listData;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet Kemasan pada baris ke-{rowIndex} - {ex.Message}");
            }
        }
    }
}

