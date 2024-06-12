using BlazorApp.IService;
using BlazorApp.Model;
using System.Net.Http.Json;

namespace BlazorApp.Service
{
    public class ItemService:IItemService
    {
        public readonly HttpClient _httpClient;

        public ItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<List<Item>> GetItems()
        {
            return await _httpClient.GetFromJsonAsync<List<Item>>("item/get-items");
        }

    }
}
