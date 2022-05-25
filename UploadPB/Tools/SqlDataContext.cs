using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using UploadPB.Models;

namespace UploadPB
{
    public class SqlDataContext<TModel> : ISqlDataContext<TModel>
    {

        private readonly SqlConnection _connection;

        public SqlDataContext(string connectionString)
        {
            _connection = CreateConnection(connectionString);
        }

        public async Task<int> ExecuteAsync(string query, TModel model)
        {
            _connection.Open();
            var result = await _connection.ExecuteAsync(query, model);
            _connection.Close();
            return result;
        }

        public async Task<int> ExecuteAsync(string query, IEnumerable<TModel> models)
        {
            if(_connection.State != ConnectionState.Open)
                _connection.Open(); 
            
            var transaction = _connection.BeginTransaction();
            var result = await _connection.ExecuteAsync(query, models, transaction : transaction);
            transaction.Commit();

            if(_connection.State == ConnectionState.Open)
                _connection.Close();
            return result;
        }

        public async Task<IEnumerable<TModel>> QueryAsync(string query)
        {
            _connection.Open();
            var result = await _connection.QueryAsync<TModel>(query);
            _connection.Close();
            return result;
        }

        public async Task<IEnumerable<TModel>> QueryAsync(string query, Object newObject)
        {
            if(_connection.State == ConnectionState.Closed)
                _connection.Open(); 
            
            var transaction = _connection.BeginTransaction();
            var result = await _connection.QueryAsync<TModel>(query, newObject, transaction : transaction);
            transaction.Commit();

            if(_connection.State == ConnectionState.Open)
                _connection.Close();
            return result;
        }

        private SqlConnection CreateConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            return new SqlConnection(connectionString);
        }
    }

    public interface ISqlDataContext<TModel>
    {
        Task<int> ExecuteAsync(string query, TModel model);
        Task<int> ExecuteAsync(string query, IEnumerable<TModel> model);
        Task<IEnumerable<TModel>> QueryAsync(string query);
        Task<IEnumerable<TModel>> QueryAsync(string query, Object newObject);
    }
}
