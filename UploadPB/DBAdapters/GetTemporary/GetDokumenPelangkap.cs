using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using UploadPB.Models;
using UploadPB.ViewModels;
//using Com.Danliris.ETL.Service.ViewModels;
using Newtonsoft.Json;
namespace UploadPB.DBAdapters.GetTemporary
{
    public class GetDokumenPelangkap
    {
        //    private readonly IDbConnection _connection;
        //    public GetDokumenPelangkap(string connectionString)
        //    {
        //        _connection = new SqlConnection(connectionString);
        //    }

        //    public async Task<List<DokumenPelengkapViewModel>> Get()
        //    {
        //        _connection.Open();
        //        var sql = "SELECT * FROM [dbo].[DOKUMENPELENGKAP_TEMP]";

        //        var result = await _connection.QueryAsync<DokumenPelengkapViewModel>(sql);

        //        return result.ToList();
        //    }
    }
}
