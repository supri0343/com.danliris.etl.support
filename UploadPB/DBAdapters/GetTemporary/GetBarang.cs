using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using UploadPB.Models.BCTemp;
using UploadPB.ViewModels;

namespace UploadPB.DBAdapters.GetTemporary
{
    public class GetBarang
    {
        //private readonly IDbConnection _connection;
        //public GetBarang(string connectionString)
        //{
        //    _connection = new SqlConnection(connectionString);
        //}

        //public async Task<List<BarangViewModel>> Get()
        //{
        //    _connection.Open();
        //    var sql = (@"SELECT * FROM [dbo].[BARANG_TEMP]");

        //    var result = await _connection.QueryAsync<BarangViewModel>(sql);

        //    return result.ToList();
        //}
    }
}
