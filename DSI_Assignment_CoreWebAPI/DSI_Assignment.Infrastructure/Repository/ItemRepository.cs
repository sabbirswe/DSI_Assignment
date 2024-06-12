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
    public class ItemRepository : IItemRepository
    {
        IDapperDbConnection _dapperDbConnection;
        public ItemRepository(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }

        /// <summary>
        /// Gets all items asynchronously.
        /// </summary>
        /// <returns>A collection of all items.</returns>
        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            using (var db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<Item>("select * from Item order by ItemName");
            }
        }
    }
}
