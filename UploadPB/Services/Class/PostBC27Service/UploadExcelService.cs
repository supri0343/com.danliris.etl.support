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
using UploadPB.Services.Interfaces.IPostBC23Service;
using UploadPB.Services.Interfaces.IPostBC27Service;

namespace UploadPB.Services.Class.PostBC27Service
{
    public class UploadExcelService : IUploadExcel27
    {
        private readonly SupportDbContext context;
        private readonly DbSet<Beacukai27Temporary> dbSet;
        private readonly DbSet<BeacukaiDocumentsModel> beacukaiDocuments;

        ConverterChecker converterChecker = new ConverterChecker();
        
        public UploadExcelService(IServiceProvider provider, SupportDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Beacukai27Temporary>();
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

                    var queryheader = (from a in ListHeader
                                       join b in ListBarang on a.NoAju equals b.NoAju
                                       join c in ListEntitas on a.NoAju equals c.NoAju
                                       select new TemporaryViewModel
                                       {
                                           ID = 0,
                                           BCNo = a.BCNo,
                                           Barang = b.Barang,
                                           Bruto = a.Bruto,
                                           CIF = a.CIF,
                                           CIF_Rupiah = b.CIF_Rupiah,
                                           JumlahSatBarang = b.JumlahSatBarang,
                                           KodeBarang = b.KodeBarang,
                                           Netto = a.Netto,
                                           NoAju = b.NoAju,
                                           NamaSupplier = c.NamaSupplier,
                                           Vendor = c.Vendor,
                                           TglBCNO = a.TglBCNO,
                                           Valuta = a.Valuta,
                                           JenisBC = a.JenisBC,
                                           JumlahBarang = ListBarang.Where(x=> x.NoAju == a.NoAju).Count(),
                                           Sat = b.Sat,
                                           KodeSupplier = c.KodeSupplier,
                                           KodeKemasan = ListKemasan.FirstOrDefault(x => x.NoAju == a.NoAju).KodeKemasan,
                                           JumlahKemasan = ListKemasan.FirstOrDefault(x => x.NoAju == a.NoAju).JumlahKemasan


                                       }).ToList();

                    var querydokumen = (from a in ListHeader
                                        join b in ListDokument on a.NoAju equals b.NoAju
                                        join c in ListEntitas on a.NoAju equals c.NoAju
                                        join d in beacukaiDocuments on b.JenisDokumen equals d.Code.ToString()
                                        select new TemporaryViewModel
                                        {
                                            ID = 0,
                                            BCNo = a.BCNo,
                                            Bruto = a.Bruto,
                                            NoAju = b.NoAju,
                                            NamaSupplier = c.NamaSupplier,
                                            Vendor = c.Vendor,
                                            TglBCNO = a.TglBCNO,
                                            Valuta = a.Valuta,
                                            JenisBC = a.JenisBC,
                                            JenisDokumen = d.Name,
                                            NomorDokumen = b.NomorDokumen,
                                            TanggalDokumen = b.TanggalDokumen,
                                            JumlahBarang = ListBarang.Where(x => x.NoAju == a.NoAju).Count()
                                        }).ToList();

                    foreach (var item in querydokumen)
                    {
                        queryheader.Add(item);
                    }

                    //delete all temporaray data
                    var itemtoDelete = context.Set<Beacukai27Temporary>();
                    context.beacukai27Temporaries.RemoveRange(itemtoDelete);
                    context.SaveChanges();
                    transaction.Commit();

                    //UploadTemporaraydData
                    Upload = await InsertToTemporary(queryheader);

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return Upload;
        }

        public async Task<int> InsertToTemporary(List<TemporaryViewModel> data)
        {
            int Created = 0;
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    long index = 1;
                    foreach(var a in data)
                    {
                        Beacukai27Temporary beacukaiTemporaryModel = new Beacukai27Temporary
                        {
                            ID = index++,
                            BCNo = a.BCNo,
                            Barang = a.Barang,
                            Bruto = a.Bruto,
                            CIF = a.CIF,
                            CIF_Rupiah = a.CIF_Rupiah,
                            JumlahSatBarang = a.JumlahSatBarang,
                            KodeBarang = a.KodeBarang,
                            Netto = a.Netto,
                            NoAju = a.NoAju,
                            NamaSupplier = a.NamaSupplier,
                            Vendor = a.Vendor,
                            TglBCNO = a.TglBCNO,
                            Valuta = a.Valuta,
                            JenisBC = a.JenisBC,
                            JumlahBarang = a.JumlahBarang,
                            Sat = a.Sat,
                            KodeSupplier = a.KodeSupplier,
                            JenisDokumen = a.JenisDokumen,
                            NomorDokumen = a.NomorDokumen,
                            TanggalDokumen = a.TanggalDokumen,
                            JumlahKemasan = a.JumlahKemasan,
                            KodeKemasan = a.KodeKemasan
                        };

                        this.dbSet.Add(beacukaiTemporaryModel);
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
                    if (sheet.Cells[rowIndex, 1].Value != null && converterChecker.GenerateValueInt(sheet.Cells[rowIndex, 3]) == 3) 
                    {
                        DataEntitas.NamaSupplier = converterChecker.GenerateValueString(sheet.Cells[rowIndex, 6]);
                        DataEntitas.KodeSupplier = converterChecker.GenerateValueString(sheet.Cells[rowIndex, 5]);
                        DataEntitas.NoAju = converterChecker.GenerateValueString(sheet.Cells[rowIndex, 1]);
                      
                    }
                    if (sheet.Cells[rowIndex, 1].Value != null && converterChecker.GenerateValueInt(sheet.Cells[rowIndex, 3]) == 8)
                    {
                        Vendor = converterChecker.GenerateValueString(sheet.Cells[rowIndex, 6]);
                    }
                    listData.Add(DataEntitas);
                }

                foreach (var a in listData)
                {
                    a.Vendor = Vendor;
                }


                return listData;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet Dokumen Pelengkap pada baris ke-{rowIndex} - {ex.Message}");
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
                    if (sheet.Cells[rowIndex, 1].Value != null)
                    {
                        listData.Add(new KemasanTemp
                        {
                            NoAju = converterChecker.GenerateValueString(sheet.Cells[rowIndex, 1]),
                            KodeKemasan = converterChecker.GenerateValueString(sheet.Cells[rowIndex, 3]),
                            JumlahKemasan = converterChecker.GenerateValueInt(sheet.Cells[rowIndex, 4]),

                        });


                    }

                }
                return listData;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memproses Sheet Dokumen Pelengkap pada baris ke-{rowIndex} - {ex.Message}");
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
                    if (sheet.Cells[rowIndex, 2].Value != null)
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
                        if (sheet.Cells[rowIndex, 4].Value == null || string.IsNullOrWhiteSpace(sheet.Cells[rowIndex, 4].Value.ToString()) || sheet.Cells[rowIndex, 4].Value.ToString() == "-")
                        {
                            //MessageBox.Show($"Row {rowIndex}: Cell 4 is blank. Please fill in the required data.");
                            throw new Exception($"Kolom Kode Barang {sheet.Cells[rowIndex, 4]} Pada Excel berisi data kosong atau tanda - ");

                            // or break; depending on your requirement
                        }

                        listData.Add(new BarangTemp(
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 1]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 5]),
                              converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex, 11]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 4]),
                              converterChecker.GenerateValueString(sheet.Cells[rowIndex, 10]),
                              converterChecker.GenerateValueDecimal(sheet.Cells[rowIndex, 27]),"",0
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
    }
}

