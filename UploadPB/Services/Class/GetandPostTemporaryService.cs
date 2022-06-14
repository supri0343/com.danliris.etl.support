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
using UploadPB.DBAdapters;
using UploadPB.DBAdapters.BeacukaiTemp;
using UploadPB.DBAdapters.GetTemporary;

namespace UploadPB.Services.Class
{
    public class GetandPostTemporaryService : IGetandPostTemporary
    {
        //static string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);
        //public IBeacukaiTemp beacukaiTemp;
        //public UploadExcelService uploadExcel;

        //public GetandPostTemporaryService(IServiceProvider provider)
        //{
        //    beacukaiTemp = provider.GetService<IBeacukaiTemp>();
        //    //uploadExcel = provider.GetService<IUploadExcel>();

        //}

        //public async Task CreateTemporary()
        //{
        //    var adapter = new GetHeaderDokumen(connectionString);
        //    var adapter1 = new GetBarang(connectionString);
        //    var adapter2 = new GetDokumenPelangkap(connectionString);
        //    var adapter3 = new BeacukaiTemp(connectionString);

        //    var HeaderDokumen = await adapter.Get();
        //    var Barang = await adapter1.Get();
        //    var DokumenPelengkap = await adapter2.Get();
        //    //var HeaderDokumen = uploadExcel.UploadBarang
        //    //var Barang = await adapter1.Get();
        //    //var DokumenPelengkap = await adapter2.Get();

        //    var queryheader = (from a in HeaderDokumen
        //                       join b in Barang on a.NoAju equals b.NoAju
        //                       select new TemporaryViewModel
        //                       {
        //                           ID =0,
        //                           BCNo = a.BCNo,
        //                           Barang = b.Barang,
        //                           Bruto = a.Bruto,
        //                           CIF = a.CIF,
        //                           CIF_Rupiah = a.CIF_Rupiah,
        //                           JumlahSatBarang = b.JumlahSatBarang,
        //                           KodeBarang = b.KodeBarang,
        //                           Netto = a.Netto,
        //                           NoAju = b.NoAju,
        //                           NamaSupplier = a.NamaSupplier,
        //                           TglBCNO = a.TglBCNO,
        //                           Valuta = a.Valuta,
        //                           JenisBC = a.JenisBC,
        //                           JumlahBarang = a.JumlahBarang,
        //                           Sat = b.Sat,
        //                           KodeSupplier = a.KodeSupplier,

        //                       }).ToList();

        //    var querydokumen = (from a in HeaderDokumen
        //                        join b in DokumenPelengkap on a.NoAju equals b.NoAju
        //                        select new TemporaryViewModel
        //                        {
        //                            ID=0,
        //                            BCNo = a.BCNo,
        //                            Bruto = a.Bruto,
        //                            NoAju = b.NoAju,
        //                            NamaSupplier = a.NamaSupplier,
        //                            TglBCNO = a.TglBCNO,
        //                            Valuta = a.Valuta,
        //                            JenisBC = a.JenisBC,
        //                            JenisDokumen = b.JenisDokumen,
        //                            NomorDokumen = b.NomorDokumen,
        //                            TanggalDokumen = b.TanggalDokumen,
        //                            JumlahBarang = a.JumlahBarang
        //                        }).ToList();

        //    //foreach (var item in querydokumen)
        //    //{
        //    //    queryheader.Add(item);
        //    //}

        //    try
        //    {
        //        if (queryheader != null)
        //        {
        //            //return new OkObjectResult(new ResponseSuccess("success", queryheader));
        //            await adapter3.DeleteBulk();
        //            await adapter3.Insert(queryheader);
        //            await adapter3.Insert(querydokumen);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Gagal menyimpan data ke Temporary - " + ex.Message);
        //    }
        //}
    }
}
