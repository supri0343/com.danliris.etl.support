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

namespace UploadPB.Services.Class
{
    public class PostBeacukaiService : IPostBeacukai
    {
        static string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);
        public IGetandPostTemporary _getandPostTemporary;

        public PostBeacukaiService(IServiceProvider provider, IGetandPostTemporary getandPostTemporary)
        {
            _getandPostTemporary = getandPostTemporary;
            //beacukaiTemp = provider.GetService<IBeacukaiTemp>();

        }

        public async Task PostBeacukai(List<TemporaryViewModel> data)
        {
            var adapter = new BeacukaiAdapter(connectionString);
            var adapter2 = new BeacukaiTemp(connectionString);
            var databc = await adapter.GetAju();
            var lastno = await adapter.GetLastNo();
            var databcc = new List<TemporaryViewModel>();
            

            foreach (var item in data)
            {
                if (!databc.Contains(item.NoAju))
                {
                    item.BCId = await GenerateNo();
                    item.Hari = DateTime.Today;

                }
                else
                {
                    //new BadRequestObjectResult(new ResponseFailed($"Gagal menyimpan data, No Aju - {item.NoAju} - sudah ada di database "));
                     throw new Exception($"Gagal menyimpan data, No Aju - {item.NoAju} - sudah ada di database.");
                }

            }

            


            foreach (var item in data)
            {
                var ver = await adapter.GetDataBC(item.NoAju);

                var index = 1;
                foreach (var detail in ver)
                {
                    detail.ID = Convert.ToInt32(lastno[0]) + index;
                    index++;
                    databcc.Add(detail);
                }


            }

            await adapter.DeleteBeacukaiTemporaryNotAll(data);
            await adapter2.Insert(databcc);

            await adapter.UpdateTemp(data);

            try
            {
                //var noaju = data.Select(x => x.NoAju).Distinct();
                await adapter.PostBC(data);
                await adapter.DeleteBeacukaiTemporary();
            }
            catch (Exception ex)
            {
                new BadRequestObjectResult(new ResponseFailed(ex.Message));
            }
            
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
