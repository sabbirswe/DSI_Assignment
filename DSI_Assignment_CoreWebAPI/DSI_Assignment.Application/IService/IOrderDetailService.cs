using DSI_Assignment.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Assignment.Application.IService
{
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetailDto>> GetAllByOrderIdAsync(int orderId);
    }
}
