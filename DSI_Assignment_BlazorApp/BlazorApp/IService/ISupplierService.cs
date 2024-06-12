using BlazorApp.Model;
using System.Net.Http.Json;

namespace BlazorApp.IService
{
    public interface ISupplierService
    {
        Task<List<Supplier>> GetSuppliers();

    }
}
