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
    public class GetHeaderDokumen
    {
        //private readonly IDbConnection _connection;
        //public GetHeaderDokumen(string connectionString)
        //{
        //    _connection = new SqlConnection(connectionString);
        //}

        //public async Task<List<HeaderDokumenTempViewModel>>Get()
        //{
        //    _connection.Open();
        //    var sql = "SELECT * FROM [dbo].[HEADER_DOKUMEN_TEMP]";

        //    var result = await _connection.QueryAsync<HeaderDokumenTempViewModel>(sql);

        //    return result.ToList();
        //}
    }
}
