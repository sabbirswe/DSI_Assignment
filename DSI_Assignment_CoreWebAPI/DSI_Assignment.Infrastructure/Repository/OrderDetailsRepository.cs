using Dapper;
using DSI_Assignment.Domain.Entities;
using DSI_Assignment.Domain.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Assignment.Infrastructure.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        IDapperDbConnection _dapperDbConnection;
        public OrderDetailRepository(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }

        /// <summary>
        /// Gets all order details by the specified order ID asynchronously.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>A collection of order details for the specified order.</returns>
        public async Task<IEnumerable<OrderDetail>> GetAllByOrderIdAsync(int orderId)
        {
            using (var db = _dapperDbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("OrderId", orderId);

                var order = await db.QueryAsync<OrderDetail>("GetOrderDetailsByOrderIdProc", parameters, commandType: CommandType.StoredProcedure);

                return order;
            }
        }

        /// <summary>
        /// Creates a new order detail asynchronously.
        /// </summary>
        /// <param name="orderDetail">The order detail entity to be created.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreateAsync(OrderDetail orderDetail)
        {
            using (var db = _dapperDbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("ItemId", orderDetail.ItemId);
                parameters.Add("OrderId", orderDetail.OrderId);
                parameters.Add("Qty", orderDetail.Qty);
                parameters.Add("Rate", orderDetail.Rate);

                 await db.ExecuteAsync("CreateOrderDetailProc", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Deletes all order details by the specified order ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the order whose details are to be deleted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteAllByOrderIdAsync(int id)
        {
            using (var db = _dapperDbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("OrderId", id);

                await db.ExecuteAsync("DeleteOrderDetailsByOrderIdProc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
