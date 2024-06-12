using BlazorApp.Model;
using System.Net.Http.Json;

namespace BlazorApp.IService
{
    public interface IItemService
    {
        Task<List<Item>> GetItems();

    }
}
