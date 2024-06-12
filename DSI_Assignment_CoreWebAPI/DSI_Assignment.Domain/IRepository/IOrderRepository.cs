using DSI_Assignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Assignment.Domain.IRepository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll(int pageSize, int pageNumber, string queryString, out int totalRows);
        Task<Order> GetByIdAsync(int id);
        Task<string> GenerateOrderRefNoAsync();
        Task<int> CreateAsync(Order order);
        Task UpdateAsync(int id,Order order);
        Task DeleteAsync(int id);
    }
}
