using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using System.Linq;
using System.Threading.Tasks;
using UploadPB.Models;
using UploadPB.ViewModels;
using UploadPB.Tools;
using UploadPB.SupporttDbContext;
using Microsoft.EntityFrameworkCore;
using UploadPB.Models.Temporary;
using UploadPB.Services.Interfaces.IPostBC25Service;

namespace UploadPB.Services.Class.PostBC25Service
{
    public class PostBeacukaiService : IPostBeacukai25
    {
        private readonly SupportDbContext context;
        private readonly DbSet<Beacukai_Temp> dbSet;
        GenerateBPNo GenerateBPNo = new GenerateBPNo();
        public PostBeacukaiService(IServiceProvider provider, SupportDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Beacukai_Temp>();
        }

        public async Task<string> PostBeacukai(List<TemporaryViewModel> data,string Username)
        {
            string Created = "";
            List<string> ajuToError = new List<string>();

            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    var existAjuBC = this.dbSet.Where(x => x.Hari.Value.Year >= (DateTime.Now.Year - 1)).Select(x => x.NoAju).Distinct();

                    var dataToPost = new List<Beacukai_Temp>();

                  
                    List<string> ajuToDeleteTemporary = new List<string>();

                    var id = "";
                    foreach (var item in data)
                    {
                        id =  await GenerateBPNo.GenerateNo();
                        if (!existAjuBC.Contains(item.NoAju))
                        {
                          
                            var ver = context.beacukai25Temporaries.Select(x => x).Where(x => x.NoAju == item.NoAju).ToList();

                            var index = 1;
                            foreach (var a in ver)
                            {
                                var lastNo = this.dbSet.Select(x => x.ID).OrderByDescending(x => x).Take(1).ToList();

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
                                    Hari = DateTime.Now
                                };

                                //addNewDataTo---BEACUKAI_TEMP
                                this.dbSet.Add(datatoPost);

                                await context.SaveChangesAsync();
                                index++;
                            }

                            ajuToDeleteTemporary.Add(item.NoAju);
                        }
                        else
                        {
                            ajuToError.Add(item.NoAju);
                        }

                    }

                    //deleteTemporary
                    var itemtoDelete = context.Set<Beacukai25Temporary>();
                    context.beacukai25Temporaries.RemoveRange(itemtoDelete);

                    //ApllyAllChange
                    await context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            if (ajuToError.Count > 0)
            {
                var ExistAju = string.Join(",", ajuToError.Select(x => x).Distinct().ToList());
                throw new Exception ($"Data dengan No Aju - {ExistAju} - tidak disimpan karena sudah ada di database.");
            }
            return Created;

        }

       






    }

}
