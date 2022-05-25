using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using UploadPB.Models.BCTemp;

namespace UploadPB.DBAdapters.Insert
{
    public class DokumenPelengkapAdapter : IDokumenPelengkapAdapter
    {
        private readonly ISqlDataContext<DokumenPelengkapTemp> context;

        public DokumenPelengkapAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<DokumenPelengkapTemp>>();
        }

        public async Task InsertBulk(IEnumerable<DokumenPelengkapTemp> models)
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
                var query = $"DELETE from [dbo].[DOKUMENPELENGKAP_TEMP]";
                await context.QueryAsync(query);
                // _connection.Close();
                //return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(IEnumerable<DokumenPelengkapTemp> model)
        {
            var query = $"INSERT INTO [dbo].[DOKUMENPELENGKAP_TEMP] ([NoAju],[JenisDokumen],[NomorDokumen],[TanggalDokumen]) VALUES(@NoAju,@JenisDokumen,@NomorDokumen,@TanggalDokumen)";
            var result = await context.ExecuteAsync(query, model);
        }
    }
}
