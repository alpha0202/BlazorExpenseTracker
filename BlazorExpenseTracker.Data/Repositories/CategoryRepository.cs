using BlazorExpenseTracker.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExpenseTracker.Data.Repositories
{
    internal class CategoryRepository : ICategoryRepository
    {

        private SqlConfiguration _connectionString;

        public CategoryRepository(SqlConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }


        public Task<bool> DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var db = dbConnection();

            var sql = @"SELECT Id, Name from Categories";

            return await db.QueryAsync<Category>(sql,new { });

        }

        public async Task<Category> GetCategoryDetails(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT Id, Name from Categories 
                                where Id = @Id";

            return await db.QueryFirstAsync<Category>(sql,new { id });
        }

        public Task<bool> InsertCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
