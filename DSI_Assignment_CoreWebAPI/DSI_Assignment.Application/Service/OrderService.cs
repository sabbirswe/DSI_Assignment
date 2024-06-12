using AutoMapper;
using DSI_Assignment.Application.DTO;
using DSI_Assignment.Application.IService;
using DSI_Assignment.Domain.Entities;
using DSI_Assignment.Domain.IRepository;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Assignment.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a paginated list of orders based on the provided criteria.
        /// </summary>
        /// <param name="pageSize">The number of orders to include per page.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="queryString">The optional query string to filter orders.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result contains a <see cref="PagedResponse{T}"/> of <see cref="OrderDto"/>.</returns>
        public async Task<PagedResponse<IEnumerable<OrderDto>>> GetAll(int pageSize, int pageNumber, string queryString)
        {
            try
            {
                int totalRows = 0;
                var orders =  _orderRepository.GetAll(pageSize, pageNumber, queryString, out totalRows);
                var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

                return new PagedResponse<IEnumerable<OrderDto>>
                {
                    Data = orderDtos,
                    TotalRows = totalRows
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching orders.");
                throw;
            }
        }

        /// <summary>
        /// Retrieves the order with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result contains the <see cref="OrderDto"/> corresponding to the specified ID.</returns>
        public async Task<OrderDto> GetByIdAsync(int id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);
                return _mapper.Map<OrderDto>(order);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred while fetching the order with ID {id}");
                throw;
            }
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="order">The <see cref="OrderDto"/> representing the order to create.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task CreateAsync(OrderDto order)
        {
            try
            {
                var newOrderId = await _orderRepository.CreateAsync(_mapper.Map<Order>(order));
                foreach (var orderDetail in order.OrderDetails)
                {
                    orderDetail.OrderId = newOrderId;
                    await _orderDetailRepository.CreateAsync(_mapper.Map<OrderDetail>(orderDetail));
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding the order");
                throw;
            }
        }

        /// <summary>
        /// Updates the order with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="order">The <see cref="OrderDto"/> representing the updated order.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task UpdateAsync(int id, OrderDto order)
        {
            try
            {
                await _orderRepository.UpdateAsync(id, _mapper.Map<Order>(order));
                if (order.IsOrderDetailsModified)
                {
                    await _orderDetailRepository.DeleteAllByOrderIdAsync(id);
                    foreach (var orderDetail in order.OrderDetails)
                    {
                        orderDetail.OrderId = id;
                        await _orderDetailRepository.CreateAsync(_mapper.Map<OrderDetail>(orderDetail));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred while updating the order with ID {id}");
                throw;
            }
        }

        /// <summary>
        /// Deletes the order with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task DeleteAsync(int id)
        {
            try
            {
                await _orderDetailRepository.DeleteAllByOrderIdAsync(id);
                await _orderRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred while deleting the order with ID {id}");
                throw;
            }
        }

        /// <summary>
        /// Generates a new order reference number.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result contains the generated order reference number.</returns>
        public async Task<string> GenerateOrderRefNoAsync()
        {
            try
            {
                return await _orderRepository.GenerateOrderRefNoAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while generating the order reference number");
                throw;
            }
        }

        /// <summary>
        /// Generates an HTML representation of the order report.
        /// </summary>
        /// <param name="order">The <see cref="OrderDto"/> representing the order.</param>
        /// <param name="orderDetails">The collection of <see cref="OrderDetailDto"/> representing the order details.</param>
        /// <returns>The HTML content of the order report.</returns>
        public string GenerateOrderReportHtml(OrderDto order, IEnumerable<OrderDetailDto> orderDetails)
        {
            try
            {
                string orderDetailHtml = "";
                foreach (var orderDetail in orderDetails)
                {
                    orderDetailHtml += $@"<tr>
                                    <td>{orderDetail.ItemName}</td>
                                    <td>{orderDetail.Qty}</td>
                                    <td>{orderDetail.Rate}</td>
                                  </tr>";
                }

                string htmlContent = $@"
            <div style=""padding:50px;"">
                <h2 style=""text-align: center;"">Purchase Order Information</h2>
                <hr>
                <table>
                    <tr>
                        <td><strong>REF. ID</strong></td>
                        <td>:</td>
                        <td>{order.RefID}</td>
                    </tr>
                    <tr>
                        <td><strong>PO. NO</strong></td>
                        <td>:</td>
                        <td>{order.PoNo}</td>
                    </tr>
                    <tr>
                        <td><strong>PO. DATE</strong></td>
                        <td>:</td>
                        <td>{order.PoDate.ToShortDateString()}</td>
                    </tr>
                    <tr>
                        <td><strong>SUPPLIER</strong></td>
                        <td>:</td>
                        <td>{order.SupplierName}</td>
                    </tr>
                    <tr>
                        <td><strong>EXPECTED DATE</strong></td>
                        <td>:</td>
                        <td>{order.ExpectedDate?.ToShortDateString()}</td>
                    </tr>
                    <tr>
                        <td><strong>REMARKS</strong></td>
                        <td>:</td>
                        <td>{order.Remarks}</td>
                    </tr>
                </table>

                <hr>
                <table style=""text-align:center;"">
                    <thead>
                        <tr>
                            <th style=""width:200px;"">ITEM NAME</th>
                            <th style=""width:100px;"">QTY.</th>
                            <th style=""width:100px;"">RATE ($)</th>
                        </tr>
                    </thead>
                    <tbody>
                        {orderDetailHtml}
                    </tbody>
                </table>
                <hr>
            </div>";

                return htmlContent;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while generating the order report HTML");
                throw;
            }
        }
    }



}
