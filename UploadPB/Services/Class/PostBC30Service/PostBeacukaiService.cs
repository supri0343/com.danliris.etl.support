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
using UploadPB.Services.Interfaces.IPostBC262Service;
using UploadPB.Services.Interfaces.IPostBC30Service;

namespace UploadPB.Services.Class.PostBC30Service
{
    public class PostBeacukaiService : IPostBeacukai30
    {
        private readonly SupportDbContext context;
        private readonly DbSet<BEACUKAI_ADDED> dbSet;

        public PostBeacukaiService(IServiceProvider provider, SupportDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<BEACUKAI_ADDED>();
        }

        public async Task<int> PostBeacukai(List<TemporaryViewModel> data,string Username)
        {
            int Created = 0;
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    var existAjuBC = this.dbSet.Select(x => x.CAR).Distinct();

                    var dataToPost = new List<BEACUKAI_ADDED>();

                    List<string> ajuToError = new List<string>();
                    List<string> ajuToDeleteTemporary = new List<string>();

                    var id = "";
                    foreach (var item in data)
                    {
                      
                        if (!existAjuBC.Contains(item.NoAju))
                        {
                          
                            var header = context.beacukai30HeaderTemporaries.Select(x => x).Where(s => s.CAR == item.NoAju).ToList();

                            var index = 1;
                            id = GenerateNo();
                            foreach (var a in header)
                            {
                                BEACUKAI_ADDED datatoPost = new BEACUKAI_ADDED
                                {
                                    BCId = id,
                                    BCType = a.BCType,
                                    BCNo = a.BCNo,
                                    CAR = a.CAR,
                                    BCDate = a.BCDate,
                                    ExpenditureNo = a.ExpenditureNo,
                                    ExpenditureDate = a.ExpenditureDate,
                                    BuyerCode = a.BuyerCode,
                                    BuyerName = a.BuyerName,
                                    Netto = a.Netto,
                                    Bruto = a.Bruto,
                                    Pack = a.Pack,
                                    CreateUser = Username,
                                    CreateDate = DateTime.Now,
                                    UpdateUser = Username,
                                    UpdateDate = DateTime.Now,
                                    Vendor = a.BuyerName,
                                    Items = new List<BEACUKAI_ADDED_DETAIL>()
                                };

                                var dataitems = context.Beacukai30ItemsTemporaries.Select(x => x).Where(x => x.CAR == a.CAR).ToList();
                                var idDetail = 1;
                                foreach (var items in dataitems)
                                {
                                    var dataitem = new BEACUKAI_ADDED_DETAIL
                                    {
                                        DetailBCId = id + idDetail.ToString().PadLeft(3, '0'),
                                        BEACUKAI_ADDED = datatoPost,
                                        CAR = items.CAR,
                                        BCId = id,
                                        ItemCode = items.ItemCode,
                                        ItemName = items.ItemName,
                                        UnitQtyCode = items.UnitQtyCode,
                                        Quantity = Convert.ToDouble(items.Quantity),
                                        Price = Convert.ToDouble(items.Price),
                                        CurrencyCode = items.CurrencyCode,
                                        UomUnit = items.UomUnit,
                                    };
                                    datatoPost.Items.Add(dataitem);
                                    idDetail++;
                                }

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

        public string GenerateNo()
        {
            var dateNow = DateTime.Now;
            var year = dateNow.ToString("yy");
            var month = dateNow.ToString("MM");
            var day = dateNow.ToString("dd");
            var hours = dateNow.ToString("hh");
            var minute = dateNow.ToString("mm");
            var second = dateNow.ToString("ss");
            var milisec = dateNow.ToString("ffffff");

            var BCId = "BC" + year + month + day + hours + minute + second + milisec;
            return BCId;

        }








    }

}
