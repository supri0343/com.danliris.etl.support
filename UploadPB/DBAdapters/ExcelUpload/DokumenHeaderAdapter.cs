using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using UploadPB.Models;

namespace UploadPB.DBAdapters.Insert
{
    public class DokumenHeaderAdapter : IDokumenHeaderAdapter
    {
        private readonly ISqlDataContext<HeaderDokumenTempModel> context;

        public DokumenHeaderAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<HeaderDokumenTempModel>>();
        }

        public async Task InsertBulk(IEnumerable<HeaderDokumenTempModel> models)
        {
            var query = $"INSERT INTO [dbo].[BEACUKAI_TEMPORARY] ([BCNo],[Barang],[Bruto],[CIF],[CIF_Rupiah],[Keterangan],[JumlahSatBarang],[KodeBarang],[KodeKemasan],[NamaKemasan],[Netto],[NoAju],[NamaSupplier],[TglDaftarAju],[TglBCNo],[Valuta],[Hari],[JenisBC],[IDHeader],[JenisDokumen],[NomorDokumen],[TanggalDokumen],[JumlahBarang],[Sat],[KodeSupplier],[TglDatang],[CreatedBy]) VALUES(@BCNo,@Barang,@Bruto,@CIF,@CIF_Rupiah,@Keterangan,@JumlahSatBarang,@KodeBarang,@KodeKemasan,@NamaKemasan,@Netto,@NoAju,@NamaSupplier,@TglDaftarAju,@TglBCNo,@Valuta,@Hari,@JenisBC,@IDHeader,@JenisDokumen,@NomorDokumen,@TanggalDokumen,@JumlahBarang,@Sat,@KodeSupplier,@TglDatang,@CreatedBy)";
            var result = await context.ExecuteAsync(query, models);

        }

        //public async Task InsertBulkItem(IEnumerable<TemporaryModel> models)
        //{
        //    var query = $"INSERT INTO [dbo].[BEACUKAI_TEMPORARY] ([BCNo],[Barang],[Bruto],[CIF],[CIF_Rupiah],[Keterangan],[JumlahSatBarang],[KodeBarang],[KodeKemasan],[NamaKemasan],[Netto],[NoAju],[NamaSupplier],[TglDaftarAju],[TglBCNo],[Valuta],[Hari],[JenisBC],[IDHeader],[JenisDokumen],[NomorDokumen],[TanggalDokumen],[JumlahBarang],[Sat],[KodeSupplier],[TglDatang],[CreatedBy]) VALUES(@BCNo,@Barang,@Bruto,@CIF,@CIF_Rupiah,@Keterangan,@JumlahSatBarang,@KodeBarang,@KodeKemasan,@NamaKemasan,@Netto,@NoAju,@NamaSupplier,@TglDaftarAju,@TglBCNo,@Valuta,@Hari,@JenisBC,@IDHeader,@JenisDokumen,@NomorDokumen,@TanggalDokumen,@JumlahBarang,@Sat,@KodeSupplier,@TglDatang,@CreatedBy)";
        //    var result = await context.ExecuteAsync(query, models);
        //}

        public async Task DeleteBulk()
        {
            try
            {
                //_connection.Open();
                var query = $"DELETE from [dbo].[HEADER_DOKUMEN_TEMP]";
                await context.QueryAsync(query);
                // _connection.Close();
                //return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(IEnumerable<HeaderDokumenTempModel> model)
        {
            var query = $"INSERT INTO [dbo].[HEADER_DOKUMEN_TEMP] ([BCNo],[Bruto],[CIF],[CIF_Rupiah],[Netto],[NoAju],[NamaSupplier],[TglBCNo],[Valuta],[JenisBC],[JumlahBarang],[KodeSupplier]) VALUES(@BCNo,@Bruto,@CIF,@CIF_Rupiah,@Netto,@NoAju,@NamaSupplier,@TglBCNo,@Valuta,@JenisBC,@JumlahBarang,@KodeSupplier)";
            var result = await context.ExecuteAsync(query, model);
        }
    }
}
