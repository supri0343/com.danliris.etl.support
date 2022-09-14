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

namespace UploadPB.Services.Class
{
    public class PostBeacukaiService : IPostBeacukai
    {
        static string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);
        public IGetandPostTemporary _getandPostTemporary;
        private readonly SupportDbContext context;
        private readonly DbSet<Beacukai_Temp> dbSet;

        public PostBeacukaiService(IServiceProvider provider, IGetandPostTemporary getandPostTemporary, SupportDbContext context)
        {
            _getandPostTemporary = getandPostTemporary;
            //beacukaiTemp = provider.GetService<IBeacukaiTemp>();
            this.context = context;
            this.dbSet = context.Set<Beacukai_Temp>();

        }

        public async Task<int> PostBeacukai(List<TemporaryViewModel> data)
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

                    //var adapter = new BeacukaiAdapter(connectionString);
                    //var adapter2 = new BeacukaiTemp(connectionString);
                    //var databc = await adapter.GetAju();
                    //var lastno = await adapter.GetLastNo();

                    //foreach (var item in data)
                    //{
                    //    if (!databc.Contains(item.NoAju))
                    //    {
                    //        item.BCId = await GenerateNo();
                    //        item.Hari = DateTime.Today;

                    //    }
                    //    else
                    //    {
                    //        //new BadRequestObjectResult(new ResponseFailed($"Gagal menyimpan data, No Aju - {item.NoAju} - sudah ada di database "));
                    //        throw new Exception($"Gagal menyimpan data, No Aju - {item.NoAju} - sudah ada di database.");
                    //    }

                    //}
                    var id = "";
                    foreach (var item in data)
                    {
                        //var ver = await adapter.GetDataBC(item.NoAju);
                        id = await GenerateNo();
                        if (!existAjuBC.Contains(item.NoAju))
                        {
                            //item.BCId = await GenerateNo();
                            //item.Hari = DateTime.Today;

                            var ver = context.BeacukaiTemporaries.Select(x => x).Where(x => x.NoAju == item.NoAju);

                            var index = 1;

                            //var nulll = null;

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
                                    Valuta = a.Valutta,
                                    JenisBC = a.JenisBC,
                                    IDHeader = Convert.ToInt32(a.IDHeader),
                                    JenisDokumen = a.JenisDokumen,
                                    NomorDokumen = a.NomorDokumen,
                                    TanggalDokumen = a.TanggalDokumen != null ? DateTime.Parse(a.TanggalDokumen.ToString()) : nullll,
                                    JumlahBarang = (int)a.JumlahBarang,
                                    Sat = a.Sat,
                                    KodeSupplier = a.KodeSupplier,
                                    TglDatang = item.TglDatang,
                                    CreatedBy = null,
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
                    var itemtoDelete = context.Set<BeacukaiTemporaryModel>().Where(x => ajuToDeleteTemporary.Contains(x.NoAju));
                    context.BeacukaiTemporaries.RemoveRange(itemtoDelete);
                    context.SaveChanges();
                    //var itemtoDelete = context.Set<BeacukaiTemporaryModel>();
                    //context.BeacukaiTemporaries.RemoveRange(itemtoDelete);
                    //context.SaveChanges();

                    //ApllyAllChange
                    Created = await context.SaveChangesAsync();
                    transaction.Commit();

                    //await adapter.DeleteBeacukaiTemporaryNotAll(data);
                    //await adapter2.Insert(dataToPost);

                    //await adapter.UpdateTemp(data);


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

            //try
            //{
            //    //var noaju = data.Select(x => x.NoAju).Distinct();
            //    await adapter.PostBC(data);
            //    await adapter.DeleteBeacukaiTemporary();
            //}
            //catch (Exception ex)
            //{
            //    new BadRequestObjectResult(new ResponseFailed(ex.Message));
            //}
            
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
            //Index++;
        }






    }

}
