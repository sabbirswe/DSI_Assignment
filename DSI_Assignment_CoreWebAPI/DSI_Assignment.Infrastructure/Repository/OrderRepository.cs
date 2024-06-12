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
    public class OrderRepository : IOrderRepository
    {
        IDapperDbConnection _dapperDbConnection;
        public OrderRepository(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }

        /// <summary>
        /// Gets all orders with pagination and search functionality.
        /// </summary>
        /// <param name="pageSize">Number of items per page.</param>
        /// <param name="pageNumber">Current page number.</param>
        /// <param name="queryString">Search query.</param>
        /// <param name="totalRows">Total number of rows found.</param>
        /// <returns>A collection of orders.</returns>
        public IEnumerable<Order> GetAll(int pageSize, int pageNumber, string queryString, out int totalRows)
        {
            using (var db = _dapperDbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("PageSize", pageSize);
                parameters.Add("PageNumber", pageNumber);
                parameters.Add("QueryString", queryString);
                parameters.Add("TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var orders =  db.Query<Order>("GetOrdersProc", parameters, commandType: CommandType.StoredProcedure);
                totalRows = parameters.Get<int>("TotalRows");
                return orders;
            }
        }

        /// <summary>
        /// Gets an order by its ID asynchronously.
        /// </summary>
        /// <param name="id">Order ID.</param>
        /// <returns>The order matching the given ID.</returns>
        public async Task<Order> GetByIdAsync(int id)
        {
            using (var db = _dapperDbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("OrderId", id);

                var order = await db.QueryFirstOrDefaultAsync<Order>("GetOrderByIdProc", parameters, commandType: CommandType.StoredProcedure);
                
                return order;
            }
        }

        /// <summary>
        /// Generates a new order reference number asynchronously.
        /// </summary>
        /// <returns>A new order reference number.</returns>
        public async Task<string> GenerateOrderRefNoAsync()
        {
            using (var db = _dapperDbConnection.CreateConnection())
            {
                var refNo= await db.ExecuteScalarAsync<string>("GenerateOrderRefNoProc", commandType: CommandType.StoredProcedure);
                return refNo;
            }
        }

        /// <summary>
        /// Creates a new order asynchronously.
        /// </summary>
        /// <param name="order">Order entity to be created.</param>
        /// <returns>The ID of the newly created order.</returns>
        public async Task<int> CreateAsync(Order order)
        {
            using (var db = _dapperDbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("SupplierId", order.SupplierId);
                parameters.Add("RefID", order.RefID);
                parameters.Add("PoNo", order.PoNo);
                parameters.Add("PoDate", order.PoDate);
                parameters.Add("ExpectedDate", order.ExpectedDate);
                parameters.Add("Remarks", order.Remarks);

                var newOrderId = await db.ExecuteScalarAsync<int>("CreateOrderProc", parameters, commandType: CommandType.StoredProcedure);
                return newOrderId;
            }
        }

        /// <summary>
        /// Updates an existing order asynchronously.
        /// </summary>
        /// <param name="id">Order ID.</param>
        /// <param name="order">Order entity with updated details.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(int id, Order order)
        {
            using (var db = _dapperDbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("OrderId", id);
                parameters.Add("SupplierId", order.SupplierId);
                parameters.Add("RefID", order.RefID);
                parameters.Add("PoNo", order.PoNo);
                parameters.Add("PoDate", order.PoDate);
                parameters.Add("ExpectedDate", order.ExpectedDate);
                parameters.Add("Remarks", order.Remarks);

                await db.ExecuteAsync("UpdateOrderProc", parameters, commandType: CommandType.StoredProcedure);
                
            }
        }

        /// <summary>
        /// Deletes an order by its ID asynchronously.
        /// </summary>
        /// <param name="id">Order ID.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(int id)
        {
            using (var db = _dapperDbConnection.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("OrderId", id);

                await db.ExecuteAsync("DeleteOrderProc", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        
    }
}
