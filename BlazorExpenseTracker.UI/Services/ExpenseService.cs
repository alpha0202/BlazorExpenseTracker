using BlazorExpenseTracker.Model;
using BlazorExpenseTracker.UI.Interfaces;
using System.Text;
using System.Text.Json;

namespace BlazorExpenseTracker.UI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly HttpClient _httpClient;

        public ExpenseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task DeleteExpense(int id)
        {
            await _httpClient.DeleteAsync($"api/Expense/{id}");
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Expense>>(
                                      await _httpClient.GetStreamAsync($"api/Expense"), 
                                      new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<Expense> GetExpenseDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Expense>(
                                      await _httpClient.GetStreamAsync($"api/Expense/{id}"), 
                                      new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task SaveExpense(Expense expense)
        {
            var expenseJson = new StringContent(JsonSerializer.Serialize(expense), Encoding.UTF8, "application/json");

            if (expense.Id == 0)
                await _httpClient.PostAsync("api/Expense", expenseJson); //insertar
            else
                await _httpClient.PutAsync("api/Expense", expenseJson); //actualizar
        }
    }
}
