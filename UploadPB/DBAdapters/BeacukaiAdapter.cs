using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System;
using UploadPB.Models;
using UploadPB.ViewModels;
//using Com.Danliris.ETL.Service.ViewModels;
using Newtonsoft.Json;

namespace UploadPB.DBAdapters
{
    public class BeacukaiAdapter
    {
        private readonly IDbConnection _connection;
        public BeacukaiAdapter(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public async Task <List<string>> GetAju ()
        {
            _connection.Open();
            var sql = "SELECT distinct NoAju FROM [dbo].[BEACUKAI_TEMP]";

            var result = await _connection.QueryAsync<string>(sql);

            return result.ToList();

            _connection.Close();
        }

        public async Task<List<string>> GetLastNo()
        {
            
            var sql = "SELECT top 1 ID FROM [dbo].[BEACUKAI_TEMP] order by ID desc";

            var result = await _connection.QueryAsync<string>(sql);

            return result.ToList();

            _connection.Close();
        }


        public async Task PostBC(List<TemporaryViewModel> models)
        {
            _connection.Open();
            foreach (var item in models)
            {
                var query = $"INSERT INTO [dbo].[BEACUKAI_TEMP] SELECT * FROM [dbo].[BEACUKAI_TEMPORARY] WHERE NoAju ='" + item.NoAju + "'";
                var result = await _connection.QueryAsync(query);
            }
            _connection.Close();
        }

        public async Task <List<TemporaryViewModel>> GetDataBC(string NoAju)
        {
          
            var query = $"SELECT * FROM [dbo].[BEACUKAI_TEMPORARY] WHERE NoAju = '"+NoAju+"' ";
            var result = await _connection.QueryAsync<TemporaryViewModel>(query);

            return result.ToList();
            _connection.Close();
        }


        public async Task DeleteBeacukaiTemporary()
        {
            try
            {
                //_connection.Open();
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

        public async Task DeleteBeacukaiTemporaryNotAll(List<TemporaryViewModel> models)
        {
            foreach (var item in models)
            {
                //_connection.Open();
                var query = $"DELETE from [dbo].[BEACUKAI_TEMPORARY] where NoAju='"+item.NoAju+"'";
                await _connection.QueryAsync(query);
                _connection.Close();
                //return true;
            }
            
            
        }

        public async Task UpdateTemp(List<TemporaryViewModel> models)
        {
            //_connection.Open();
            foreach (var item in models)
            {
                string bc = item.BCId;
                string hari = item.Hari != null ? item.Hari.GetValueOrDefault().ToString("yyyy/MM/dd") : item.Hari.GetValueOrDefault().ToString("yyyy/MM/dd");
                string datang = item.TglDatang != null ? item.TglDatang.GetValueOrDefault().ToString("yyyy/MM/dd") : item.TglDatang.GetValueOrDefault().ToString("yyyy/MM/dd");

                //var query = "UPDATE [dbo].[BEACUKAI_TEMPORARY] set ([BCId],[Hari],[TglDatang]) VALUES(@BCId,@Hari,@TglDatang) WHERE NoAju = '"+ noaju+"' ";
                var query = "UPDATE [dbo].[BEACUKAI_TEMPORARY] set [BCId] = '"+bc+"', [Hari]='"+hari+"', [TglDatang]='"+datang+"' WHERE NoAju = '" + item.NoAju + "' ";
                var result = await _connection.ExecuteAsync(query, models);
            }
            _connection.Close();
        }
    }
}
