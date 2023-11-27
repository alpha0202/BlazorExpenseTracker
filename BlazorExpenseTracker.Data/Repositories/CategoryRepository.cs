using BlazorExpenseTracker.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExpenseTracker.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        //private SqlConfiguration _connectionString;
        private IDbConnection _dbConnection;


        public CategoryRepository(SqlConfiguration connectionString, IDbConnection dbConnection)
        {
            //_connectionString = connectionString;
            _dbConnection = dbConnection;
        }

        //protected SqlConnection dbConnection()
        //{
        //    var dbConn = new SqlConnection(_connectionString.ConnectionString);
        //    return dbConn;
        //}


      
        //get all
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            //var db = dbConnection();

            var sql = @"SELECT Id, Name from Categories";

            var result =  await _dbConnection.QueryAsync<Category>(sql,new { });
            return result;
        }

        //get by id
        public async Task<Category> GetCategoryDetails(int id)
        {
            //var db = dbConnection();
            var sql = @"SELECT Id, Name 
                                from Categories 
                                where Id = @Id";

            return await _dbConnection.QueryFirstOrDefaultAsync<Category>(sql,new { Id = id });
        }

        //insert
        public async Task<bool> InsertCategory(Category category)
        {
            //var db = dbConnection();
            var sql = @"INSERT INTO Categories(Name)
                         values(@Name)";

            var result = await _dbConnection.ExecuteAsync(sql, new { category.Name });

            return result > 0 && result < 1;
        }

        //update
        public async Task<bool> UpdateCategory(Category category)
        {
            //var db = dbConnection();
            var sql = @"UPDATE Categories set Name = @Name
                                            where Id = @Id";

            var result = await _dbConnection.ExecuteAsync(sql, new {category.Name, category.Id});

            return result > 0 && result < 1;
        }


        //delete
        public async Task<bool> DeleteCategory(int id)
        {
            //var db = dbConnection();
            var sql = @"DELETE Categories where Id = @Id";

            var result = await _dbConnection.ExecuteAsync( sql, new { Id = id });

            return result > 0 && result < 1;
        }
    }
}
