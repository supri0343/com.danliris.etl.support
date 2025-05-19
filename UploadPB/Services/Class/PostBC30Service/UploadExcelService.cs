using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using System.Linq;
using System.Threading.Tasks;
using UploadPB.Models;
using UploadPB.Models.BCTemp;
using UploadPB.Tools;
using UploadPB.DBAdapters;
using UploadPB.DBAdapters.Insert;
using UploadPB.ViewModels;
using UploadPB.SupporttDbContext;
using Microsoft.EntityFrameworkCore;
using UploadPB.Models.Temporary;
using UploadPB.Services.Interfaces.IPostBC30Service;
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace UploadPB.Services.Class.PostBC30Service
{
    public class UploadExcelService : IUploadExcel30
    {
        private readonly SupportDbContext context;
        private readonly DbSet<Beacukai30HeaderTemporary> dbSet;
        private readonly DbSet<BeacukaiDocumentsModel> beacukaiDocuments;

        ConverterChecker converterChecker = new ConverterChecker();
        
        public UploadExcelService(IServiceProvider provider, SupportDbContext context)
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

                    var ListHeader = new List<HeaderDokumenTempModel>();
                    var ListBarang = new List<BarangTemp30>();
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
                                         Pack = b.Pack
                                     }).ToList();
                    //Old Query Header
                    //var queryheader = ListKemasan.Count == 1 ? (from a in ListHeader
                    //                                            join b in ListKemasan on a.NoAju equals b.NoAju
                    //                                            //join c in ListEntitas on a.NoAju equals c.NoAju
                    //                                            join d in ListDokument on a.NoAju equals d.NoAju
                    //                                            //join e in ListBarang on a.NoAju equals e.NoAju
                    //                                            select new Beacukai30HeaderTemporary
                    //                                            {
                    //                                                BCId = "",
                    //                                                BCType = "BC 3.0",
                    //                                                BCNo = a.BCNo,
                    //                                                CAR = a.NoAju,
                    //                                                BCDate = a.TglBCNO.Value.DateTime,
                    //                                                ExpenditureNo = d.NomorDokumen,
                    //                                                ExpenditureDate = d.TanggalDokumen.Value.DateTime,
                    //                                                BuyerCode = "-",
                    //                                                BuyerName = a.NamaSupplier,
                    //                                                Vendor = a.NamaSupplier,
                    //                                                Netto = Convert.ToDouble(a.Netto),
                    //                                                Bruto = Convert.ToDouble(a.Bruto),
                    //                                                Pack = b.KodeKemasan,
                    //                                                Country = a.Country
                    //                                                //Items = queryitem
                    //                                            }).ToList() : (from a in ListHeader
                    //                                                           //join b in ListKemasan on a.NoAju equals b.NoAju
                    //                                                           //join c in ListEntitas on a.NoAju equals c.NoAju
                    //                                                           join d in ListDokument on a.NoAju equals d.NoAju
                    //                                                           join e in ListBarang on a.NoAju equals e.NoAju
                    //                                                           select new Beacukai30HeaderTemporary
                    //                                                           {
                    //                                                               BCId = "",
                    //                                                               BCType = "BC 3.0",
                    //                                                               BCNo = a.BCNo,
                    //                                                               CAR = a.NoAju,
                    //                                                               BCDate = a.TglBCNO.Value.DateTime,
                    //                                                               ExpenditureNo = d.NomorDokumen,
                    //                                                               ExpenditureDate = d.TanggalDokumen.Value.DateTime,
                    //                                                               BuyerCode = "-",
                    //                                                               BuyerName = a.NamaSupplier,
                    //                                                               Vendor = a.NamaSupplier,
                    //                                                               Netto = Convert.ToDouble(e.Netto),
                    //                                                               Bruto = Convert.ToDouble(a.Bruto),
                    //                                                               Pack = e.Pack,
                    //                                                               Country = a.Country
                    //                                                               //Netto = Convert.ToDouble(e.Netto),
                    //                                                               //Bruto = Convert.ToDouble(a.Bruto),
                    //                                                               //Pack = b.KodeKemasan,
                    //                                                               //Items = queryitem
                    //                                                           }).ToList();

                    //new query Header
                    var newHeader = new List<Beacukai30HeaderTemporary>();

                    foreach(var header in ListHeader)
                    {
                        Beacukai30HeaderTemporary dToPush = new Beacukai30HeaderTemporary();

                        //Check jika kamasan dalam no aju yang sama cuma 1
                        var kemasan = ListKemasan.Where(s => s.NoAju == header.NoAju).ToList();

                        //check list dokumen
                        var dok = ListDokument.Where(s => s.NoAju == header.NoAju).FirstOrDefault();

                        //if kemasan == 1
                        if(kemasan.Count == 1)
                        {
                            dToPush.BCId = "";
                            dToPush.BCType = "BC 3.0";
                            dToPush.BCNo = header.BCNo;
                            dToPush.CAR = header.NoAju;
                            dToPush.BCDate = header.TglBCNO.Value.DateTime;
                            dToPush.ExpenditureNo = dok?.NomorDokumen;
                            dToPush.ExpenditureDate = dok != null ? dok.TanggalDokumen.Value.DateTime : DateTime.MinValue;
                            dToPush.BuyerCode = "-";
                            dToPush.BuyerName = header.NamaSupplier;
                            dToPush.Vendor = header.NamaSupplier;
                            dToPush.Netto = Convert.ToDouble(header.Netto);
                            dToPush.Bruto = Convert.ToDouble(header.Bruto);
                            dToPush.Pack = kemasan[0].KodeKemasan;
                            dToPush.Country = header.Country;

                            newHeader.Add(dToPush);
                        }
                        //if kemasan > 1
                        else
                        {
                            //get data barang by AJU
                            var listBarang = ListBarang.Where(s => s.NoAju == header.NoAju).ToList();

                            foreach(var barang in listBarang)
                            {
                                dToPush.BCId = "";
                                dToPush.BCType = "BC 3.0";
                                dToPush.BCNo = header.BCNo;
                                dToPush.CAR = header.NoAju;
                                dToPush.BCDate = header.TglBCNO.Value.DateTime;
                                dToPush.ExpenditureNo = dok?.NomorDokumen;
                                dToPush.ExpenditureDate = dok != null ? dok.TanggalDokumen.Value.DateTime : DateTime.MinValue;
                                dToPush.BuyerCode = "-";
                                dToPush.BuyerName = header.NamaSupplier;
                                dToPush.Vendor = header.NamaSupplier;
                                dToPush.Netto = Convert.ToDouble(barang.Netto);
                                dToPush.Bruto = Convert.ToDouble(header.Bruto);
                                dToPush.Pack = barang.Pack;
                                dToPush.Country = header.Country;

                                newHeader.Add(dToPush);
                            }
                        }
                    }

                    //delete all temporaray data
                    var dataHeader = context.Set<Beacukai30HeaderTemporary>();
                    context.beacukai30HeaderTemporaries.RemoveRange(dataHeader);
                    context.SaveChanges();

                    var itemtoDelete = context.Set<Beacukai30ItemsTemporary>();
                    context.Beacukai30ItemsTemporaries.RemoveRange(itemtoDelete);
                    context.SaveChanges();

                    transaction.Commit();

                //UploadTemporaraydData
                Upload = await InsertToTemporary(newHeader, queryitem);

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
                  
                        this.dbSet.Add(a);
                        long indexItem = 1;
                        foreach (var item in items.Where(x => x.Pack == a.Pack && x.CAR == a.CAR))
                        {
                            item.DetailBCId = index.ToString() + indexItem.ToString();
                            item.BCId = a.BCId;
                            indexItem++;
                            context.Beacukai30ItemsTemporaries.Add(item);
                            await context.SaveChangesAsync();
                        }
                        await context.SaveChangesAsync();
                        index++;
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


        public List<HeaderDokumenTempModel> UploadHeader(ExcelWorksheets excel, int data)
        {
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<HeaderDokumenTempModel>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = 2; rowIndex <= totalRow; rowIndex++)
                {
                    if (sheet.Cells[rowIndex, 94].Value != null)
                    {
                        listData.Add(new HeaderDokumenTempModel(
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
                            ""//Vendor
                            )
                        { Country = converterChecker.GenerateValueString(sheet.Cells[rowIndex, 34]) });
                    }
                }
                return listData;
            }


            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet Header Dokumen pada baris ke-{rowIndex} - {ex.Message}");
            }
           
            
        }

        public List<BarangTemp30> UploadBarang(ExcelWorksheets excel, int data)
        {
            var sheet = excel[data];
            var totalRow = sheet.Dimension.Rows;
            var listData = new List<BarangTemp30>();
            int rowIndex = 0;
            try
            {
                for (rowIndex = 2; rowIndex <= totalRow; rowIndex++)
                {
                    if (sheet.Cells[rowIndex, 2].Value != null)
                    {
                        if (sheet.Cells[rowIndex, 4].Value == null || string.IsNullOrWhiteSpace(sheet.Cells[rowIndex, 4].Value.ToString()) || sheet.Cells[rowIndex, 4].Value.ToString() == "-")
                        {
                            //MessageBox.Show($"Row {rowIndex}: Cell 4 is blank. Please fill in the required data.");
                            throw new Exception($"Kolom Kode Barang {sheet.Cells[rowIndex, 4]} Pada Excel berisi data kosong atau tanda - ");

                            // or break; depending on your requirement
                        }

                        listData.Add(new BarangTemp30(
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 1]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 5]),
                              converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex, 11]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 4]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 10]),
                              converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex, 29]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 12]),
                              converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex, 20]),
                              converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex, 20])
                            ));
                    }
                }
                return listData;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet BARANG, baris ke- {rowIndex}, - {ex.Message}");
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

