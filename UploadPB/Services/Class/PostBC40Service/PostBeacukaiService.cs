using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using System.Linq;
using System.Threading.Tasks;
using UploadPB.Services.Interfaces;
using UploadPB.Models;
using UploadPB.ViewModels;
using UploadPB.Models.BCTemp;
using UploadPB.Tools;
using Microsoft.AspNetCore.Mvc;
using UploadPB.DBAdapters;
using UploadPB.DBAdapters.BeacukaiTemp;
using UploadPB.DBAdapters.GetTemporary;
using UploadPB.SupporttDbContext;
using Microsoft.EntityFrameworkCore;
using UploadPB.Services.Interfaces.Post40;
using UploadPB.Models.Temporary;

namespace UploadPB.Services.Class.Post40
{
    public class PostBeacukaiService : IPostBeacukai
    {
        static string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);
    
        private readonly SupportDbContext context;
        private readonly DbSet<Beacukai_Temp> dbSet;

        public PostBeacukaiService(IServiceProvider provider, SupportDbContext context)
        {
          
            //beacukaiTemp = provider.GetService<IBeacukaiTemp>();
            this.context = context;
            this.dbSet = context.Set<Beacukai_Temp>();

        }

        public async Task<int> PostBeacukai(List<TemporaryViewModel> data,string Username)
        {
            int Created = 0;
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    var existAjuBC = this.dbSet.Select(x => x.NoAju).Distinct();

                    var lastNo = this.dbSet.Select(x => x.ID).OrderByDescending(x => x).Take(1).ToArray();

                    var dataToPost = new List<Beacukai_Temp>();

                    List<string> ajuToError = new List<string>();
                    List<string> ajuToDeleteTemporary = new List<string>();

                    var id = "";
                    foreach (var item in data)
                    {
                        id = await GenerateNo();
                        if (!existAjuBC.Contains(item.NoAju))
                        {
                          
                            var ver = context.beacukai40Temporaries.Select(x => x).Where(x => x.NoAju == item.NoAju);

                            var index = 1;
                            foreach (var a in ver)
                            {
                                DateTime TglBC = DateTime.Parse(a.TglBCNO.ToString());
                                DateTime? nullll = null;

                                Beacukai_Temp datatoPost = new Beacukai_Temp
                                {
                                    ID = Convert.ToInt32(lastNo[0] + index),
                                    BCId = id,
                                    BCNo = a.BCNo,
                                    Barang = a.Barang,
                                    Bruto = a.Bruto,
                                    CIF = a.CIF,
                                    CIF_Rupiah = a.CIF_Rupiah,
                                    Keterangan = a.Keterangan,
                                    JumlahSatBarang = a.JumlahSatBarang,
                                    KodeBarang = a.KodeBarang,
                                    KodeKemasan = a.KodeKemasan,
                                    NamaKemasan = a.NamaKemasan,
                                    Netto = a.Netto,
                                    NoAju = a.NoAju,
                                    NamaSupplier = a.NamaSupplier,
                                    TglDaftarAju = a.TglDaftarAju,
                                    TglBCNO = a.TglBCNO != null ? TglBC : nullll,
                                    Valuta = a.Valuta,
                                    JenisBC = a.JenisBC,
                                    IDHeader = Convert.ToInt32(a.IDHeader),
                                    JenisDokumen = a.JenisDokumen,
                                    NomorDokumen = a.NomorDokumen,
                                    TanggalDokumen = a.TanggalDokumen != null ? DateTime.Parse(a.TanggalDokumen.ToString()) : nullll,
                                    JumlahBarang = (int)a.JumlahBarang,
                                    Sat = a.Sat,
                                    KodeSupplier = a.KodeSupplier,
                                    TglDatang = item.TglDatang,
                                    CreatedBy = Username,
                                    Vendor = a.Vendor,
                                    Hari = DateTime.Today
                                };

                                //addNewDataTo---BEACUKAI_TEMP
                                this.dbSet.Add(datatoPost);
                                index++;
                            }

                            ajuToDeleteTemporary.Add(item.NoAju);
                        }
                        else
                        {
                            ajuToError.Add(item.NoAju);
                        }

                    }

                    //deleteTemporaryForSelectedData
                    var itemtoDelete = context.Set<Beacukai40Temporary>().Where(x => ajuToDeleteTemporary.Contains(x.NoAju));
                    context.beacukai40Temporaries.RemoveRange(itemtoDelete);       

                    //ApllyAllChange
                    Created = await context.SaveChangesAsync();
                    transaction.Commit();

                    if (ajuToError.Count > 0)
                    {
                        var ExistAju = string.Join(",", ajuToError.Select(x => x).Distinct().ToList());
                        throw new Exception($"Data dengan No Aju - {ExistAju} - tidak disimpan karena sudah ada di database.");
                    }
                }
                catch (Exception e)
                {
                    //transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return Created;
            
        }

        public async Task<string> GenerateNo()
        {
            //var Index = 0;
            var date = DateTime.UtcNow;
            //var date2 = DateTime.Today();

            var bp = "BP";
            var year = (date.Year.ToString()).Substring(2,2);
            var mounth = date.ToString("MM");
            var day = date.ToString("dd");
            var hour = date.ToString("HH");
            var minute = date.ToString("mm");
            var sec = date.ToString("ss");
            var msec = date.ToString("ffffff");

            string no = string.Concat(bp, year, mounth, day, hour, minute, sec, msec);

            return no;
            
        }






    }

}
