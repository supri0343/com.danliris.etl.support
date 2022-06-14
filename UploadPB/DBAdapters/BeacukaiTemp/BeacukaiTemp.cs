using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using UploadPB.Models;
using UploadPB.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using UploadPB.Models.BCTemp;
using UploadPB.ViewModels;

namespace UploadPB.DBAdapters.BeacukaiTemp
{
    public class BeacukaiTemp : IBeacukaiTemp
    {
        private readonly ISqlDataContext<TemporaryViewModel> context;
        private readonly IDbConnection _connection;

        //public BeacukaiTemp (IServiceProvider service )
        //{
        //    context = service.GetService<ISqlDataContext<TemporaryViewModel>>();
        //}

        public BeacukaiTemp(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public async Task InsertBulk(IEnumerable<TemporaryViewModel> models)
        {
            var query = $"INSERT INTO [dbo].[BEACUKAI_TEMPORARY] ([BCNo],[Barang],[Bruto],[CIF],[CIF_Rupiah],[Keterangan],[JumlahSatBarang],[KodeBarang],[KodeKemasan],[NamaKemasan],[Netto],[NoAju],[NamaSupplier],[TglDaftarAju],[TglBCNo],[Valuta],[Hari],[JenisBC],[IDHeader],[JenisDokumen],[NomorDokumen],[TanggalDokumen],[JumlahBarang],[Sat],[KodeSupplier],[TglDatang],[CreatedBy]) VALUES(@BCNo,@Barang,@Bruto,@CIF,@CIF_Rupiah,@Keterangan,@JumlahSatBarang,@KodeBarang,@KodeKemasan,@NamaKemasan,@Netto,@NoAju,@NamaSupplier,@TglDaftarAju,@TglBCNo,@Valuta,@Hari,@JenisBC,@IDHeader,@JenisDokumen,@NomorDokumen,@TanggalDokumen,@JumlahBarang,@Sat,@KodeSupplier,@TglDatang,@CreatedBy)";
            var result = await context.ExecuteAsync(query, models);

        }

        //public async Task InsertBulkItem(IEnumerable<TemporaryModel> models)
        //{
        //    var query = $"INSERT INTO [dbo].[BEACUKAI_TEMPORARY] ([BCNo],[Barang],[Bruto],[CIF],[CIF_Rupiah],[Keterangan],[JumlahSatBarang],[KodeBarang],[KodeKemasan],[NamaKemasan],[Netto],[NoAju],[NamaSupplier],[TglDaftarAju],[TglBCNo],[Valuta],[Hari],[JenisBC],[IDHeader],[JenisDokumen],[NomorDokumen],[TanggalDokumen],[JumlahBarang],[Sat],[KodeSupplier],[TglDatang],[CreatedBy]) VALUES(@BCNo,@Barang,@Bruto,@CIF,@CIF_Rupiah,@Keterangan,@JumlahSatBarang,@KodeBarang,@KodeKemasan,@NamaKemasan,@Netto,@NoAju,@NamaSupplier,@TglDaftarAju,@TglBCNo,@Valuta,@Hari,@JenisBC,@IDHeader,@JenisDokumen,@NomorDokumen,@TanggalDokumen,@JumlahBarang,@Sat,@KodeSupplier,@TglDatang,@CreatedBy)";
        //    var result = await context.ExecuteAsync(query, models);
        //}

        public async Task Insert(IEnumerable<TemporaryViewModel> models)
        {
            _connection.Open();
            var query = $"INSERT INTO [dbo].[BEACUKAI_TEMPORARY] ([ID],[BCNo],[Barang],[Bruto],[CIF],[CIF_Rupiah],[Keterangan],[JumlahSatBarang],[KodeBarang],[KodeKemasan],[NamaKemasan],[Netto],[NoAju],[NamaSupplier],[TglDaftarAju],[TglBCNo],[Valuta],[Hari],[JenisBC],[IDHeader],[JenisDokumen],[NomorDokumen],[TanggalDokumen],[JumlahBarang],[Sat],[KodeSupplier],[TglDatang],[CreatedBy],[Vendor]) VALUES(@ID,@BCNo,@Barang,@Bruto,@CIF,@CIF_Rupiah,@Keterangan,@JumlahSatBarang,@KodeBarang,@KodeKemasan,@NamaKemasan,@Netto,@NoAju,@NamaSupplier,@TglDaftarAju,@TglBCNo,@Valuta,@Hari,@JenisBC,@IDHeader,@JenisDokumen,@NomorDokumen,@TanggalDokumen,@JumlahBarang,@Sat,@KodeSupplier,@TglDatang,@CreatedBy,@Vendor)";
            var result = await _connection.ExecuteAsync(query, models);
            _connection.Close();
        }

        public async Task<List<TemporaryViewModel>>Get()
        {
            var query = "select distinct ID,NoAju,BCNo,TglBCNO from BEACUKAI_TEMPORARY order by NoAju,BCNo";
            var result = await _connection.QueryAsync<TemporaryViewModel>(query);

            return result.ToList();
        }

        public async Task DeleteBulk()
        {
            try
            {
                _connection.Open();
                var query = $"DELETE from [dbo].[BEACUKAI_TEMPORARY]";
                await _connection.QueryAsync(query);
                _connection.Close();
                //return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task DeleteBarangTemp()
        //{
        //    try
        //    {
        //        _connection.Open();
        //        var query = $"DELETE from [dbo].[BARANG_TEMP]";
        //        await _connection.QueryAsync(query);
        //        _connection.Close();
        //        //return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task DeleteDokumentTemp()
        //{
        //    try
        //    {
        //        _connection.Open();
        //        var query = $"DELETE from [dbo].[HEADER_DOKUMEN_TEMP]";
        //        await _connection.QueryAsync(query);
        //        _connection.Close();
        //        //return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task DeleteDokumentPelengTemp()
        //{
        //    try
        //    {
        //        _connection.Open();
        //        var query = $"DELETE from [dbo].[DOKUMENPELENGKAP_TEMP]";
        //        await _connection.QueryAsync(query);
        //        _connection.Close();
        //        //return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}
