using BlazorApp.Model;
using System.Net.Http.Json;

namespace BlazorApp.IService
{
    public interface IOrderService
    {
        Task<PagedResponse<List<Order>>> GetOrders(int pageSize, int pageNumber, string? queryString);
        Task<Order> GetOrder(int id);
        Task<string> GenerateOrderRefNo();
        Task<HttpResponseMessage> CreateOrder(Order order);
        Task<HttpResponseMessage> UpdateOrder(int id, Order order);
        Task<HttpResponseMessage> DeleteOrder(int id);

    }
}
