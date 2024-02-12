using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using System.Linq;
using System.Threading.Tasks;
using UploadPB.ViewModels;
using UploadPB.Tools;
using Microsoft.EntityFrameworkCore;
using UploadPB.Models.Temporary;
using UploadPB.Services.Interfaces.IPostBC20Service.PostAG;
using UploadPB.SupporttDbContext.AG;
using UploadPB.Models.Temporary.AGSupport;
using UploadPB.Models.AGSupport;

namespace UploadPB.Services.Class.PostBC20Service.PostAG
{
    public class PostBeacukaiService : IPostBeacukai20
    {
        private readonly AGDbContext context;
        private readonly DbSet<Beacukai_Temp> dbSet;
        GenerateBPNo GenerateBPNo = new GenerateBPNo();
        public PostBeacukaiService(IServiceProvider provider, AGDbContext context)
        {
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
                    var existAjuBC = this.dbSet.Where(x => x.Hari.Value.Year >= (DateTime.Now.Year - 1 )).Select(x => x.NoAju).Distinct();

                    var dataToPost = new List<Beacukai_Temp>();

                    List<string> ajuToError = new List<string>();
                    List<string> ajuToDeleteTemporary = new List<string>();

                    var id = "";
                    foreach (var item in data)
                    {
                        id =  await GenerateBPNo.GenerateNo();
                        if (!existAjuBC.Contains(item.NoAju))
                        {
                          
                            var ver = context.Beacukai20Temporary.Select(x => x).Where(x => x.NoAju == item.NoAju).ToList();

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
                                    Hari = DateTime.Now,
                                    SeriBarang = a.SeriBarang
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
                    var itemtoDelete = context.Set<Beacukai20Temporary>();
                    context.Beacukai20Temporary.RemoveRange(itemtoDelete);       

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

       






    }

}
