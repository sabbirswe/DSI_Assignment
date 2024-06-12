using BlazorApp.IService;
using BlazorApp.Model;
using System.Net.Http.Json;

namespace BlazorApp.Service
{
    public class OrderService:IOrderService
    {
        public readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagedResponse<List<Order>>> GetOrders(int pageSize, int pageNumber, string? queryString)
        {
            return await _httpClient.GetFromJsonAsync<PagedResponse<List<Order>>>($"order/get-orders?pagesize={pageSize}&pageNumber={pageNumber}&queryString={queryString}");
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _httpClient.GetFromJsonAsync<Order>($"order/get-order/{id}");
        }

        public async Task<string> GenerateOrderRefNo()
        {
            return await _httpClient.GetStringAsync($"order/generate-refno");
        }
        public async Task<HttpResponseMessage> CreateOrder(Order order)
        {
            return await _httpClient.PostAsJsonAsync("order/create-order", order);
        }

        public async Task<HttpResponseMessage> UpdateOrder(int id, Order order)
        {
            return await _httpClient.PutAsJsonAsync($"order/update-order/{id}", order);
        }

        public async Task<HttpResponseMessage> DeleteOrder(int id)
        {
            return await _httpClient.DeleteAsync($"order/delete-order/{id}");
        }
    }
}
