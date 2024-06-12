using AutoMapper;
using DSI_Assignment.Application.DTO;
using DSI_Assignment.Application.IService;
using DSI_Assignment.Domain.Entities;
using DSI_Assignment.Domain.IRepository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Assignment.Application.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        public OrderDetailService(IOrderDetailRepository OrderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = OrderDetailRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all order details associated with the specified order ID.
        /// </summary>
        /// <param name="orderId">The ID of the order to retrieve order details for.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result contains a collection of <see cref="OrderDetailDto"/> representing the order details.</returns>
        public async Task<IEnumerable<OrderDetailDto>> GetAllByOrderIdAsync(int orderId)
        {
            try
            {
                var orderDetails = await _orderDetailRepository.GetAllByOrderIdAsync(orderId);
                return _mapper.Map<IEnumerable<OrderDetailDto>>(orderDetails);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred while fetching order details for order with ID {orderId}");
                throw;
            }
        }

    }
}
