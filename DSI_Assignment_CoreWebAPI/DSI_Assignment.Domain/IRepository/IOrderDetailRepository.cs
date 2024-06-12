using DSI_Assignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Assignment.Domain.IRepository
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetail>> GetAllByOrderIdAsync(int orderId);
        Task CreateAsync(OrderDetail orderDetail);
        Task DeleteAllByOrderIdAsync(int id);
    }
}
