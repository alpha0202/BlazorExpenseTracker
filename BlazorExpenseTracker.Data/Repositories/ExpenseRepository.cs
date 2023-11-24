using BlazorExpenseTracker.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExpenseTracker.Data.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IDbConnection _dbConnection;

        public ExpenseRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> DeleteExpense(int id)
        {
            var sql = @"DELETE Expenses where Id = @Id";

            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            var sql = @"SELECT e.Id, Amount, CategoryId, ExpenseType, TransactionDate, 
                               c.Id, c.Name
                        FROM Expenses e
                        INNER JOIN Categories c ON e.CategoryId = c.Id";

            var result = await _dbConnection.QueryAsync<Expense, Category, Expense>(sql, ((expense, category) =>
            {
                expense.Category = category;
                return expense;

            }), new { }, splitOn: "Id");

            return result;
        }

        public async Task<Expense> GetExpenseDetail(int id)
        {
            var sql = @"SELECT Id, Amount, CategoryId,ExpenseType, TransactionDate FROM Expenses where Id = @Id";

            var result = await _dbConnection.QueryFirstOrDefaultAsync<Expense>(sql, new { Id = id });
            return result;
        }

        public async Task<bool> InsertExpenseDetails(Expense expense)
        {
            var sql = @"INSERT INTO Expenses(Amount, CategoryId, ExpenseType, TransactionDate)
                                    VALUES(@Amount,@CategoryId,@ExpenseType,@TransactionDate)";

            var result = await _dbConnection.ExecuteAsync(sql, new
            {
                expense.Amount,
                expense.CategoryId,
                expense.ExpenseType,
                expense.TransactionDate
            });

            return result > 0;
        }

        public async Task<bool> UpdateExpense(Expense expense)
        {
            var sql = @"UPDATE Expenses
                                SET Amount = @Amount,
                                    CategoryId = @CategoryId,
                                    ExpenseType = @ExpenseType,
                                    TransactionDate = @TransactionDate
                                WHERE Id = @Id";

            var result = await _dbConnection.ExecuteAsync(sql, new
            {
                expense.Id,
                expense.Amount,
                expense.CategoryId,
                expense.ExpenseType,
                expense.TransactionDate
            });

            return result > 0;
        }
    }
}
