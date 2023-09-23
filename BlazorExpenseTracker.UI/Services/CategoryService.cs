using BlazorExpenseTracker.Model;
using BlazorExpenseTracker.UI.Interfaces;
using System.Text;
using System.Text.Json;

namespace BlazorExpenseTracker.UI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task DeleteCategory(int id)
        {
            await _httpClient.DeleteAsync($"api/Category/{id}");

        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
           return await JsonSerializer.DeserializeAsync<IEnumerable<Category>>(
                                       await _httpClient.GetStreamAsync($"api/Category"),new JsonSerializerOptions { PropertyNameCaseInsensitive= true});
           
        }

        public async Task<Category> GetCategoryDetails(int id)
        {
           return await JsonSerializer.DeserializeAsync<Category>(
                                       await _httpClient.GetStreamAsync($"api/Category/{id}"), new JsonSerializerOptions { PropertyNameCaseInsensitive= true});
           
        }

        public async Task SaveCategory(Category category)
        {
           var CategoryJson = new StringContent(JsonSerializer.Serialize(category),Encoding.UTF8, "application/json");

            if (category.Id == 0)
              await  _httpClient.PostAsync("api/Category", CategoryJson);
            else
              await  _httpClient.PutAsync("api/Category", CategoryJson);

        }
    }
}
