using DSI_Assignment.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Assignment.Application.IService
{
    public interface IOrderService
    {
        Task<PagedResponse<IEnumerable<OrderDto>>> GetAll(int pageSize, int pageNumber, string queryString);
        Task<OrderDto> GetByIdAsync(int id);
        Task CreateAsync(OrderDto order);
        Task UpdateAsync(int id,OrderDto order);
        Task DeleteAsync(int id);
        Task<string> GenerateOrderRefNoAsync();
        string GenerateOrderReportHtml(OrderDto order, IEnumerable<OrderDetailDto> orderDetails);
    }
}
