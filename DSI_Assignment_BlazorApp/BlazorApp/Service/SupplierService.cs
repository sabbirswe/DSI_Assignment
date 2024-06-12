using BlazorApp.IService;
using BlazorApp.Model;
using System.Net.Http.Json;

namespace BlazorApp.Service
{
    public class SupplierService:ISupplierService
    {
        public readonly HttpClient _httpClient;

        public SupplierService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<List<Supplier>> GetSuppliers()
        {
            return await _httpClient.GetFromJsonAsync<List<Supplier>>("supplier/get-suppliers");
        }
    }
}
