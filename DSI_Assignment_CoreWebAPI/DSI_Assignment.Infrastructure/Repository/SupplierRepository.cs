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
    public class SupplierRepository : ISupplierRepository
    {
        IDapperDbConnection _dapperDbConnection;
        public SupplierRepository(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }

        /// <summary>
        /// Gets all suppliers asynchronously.
        /// </summary>
        /// <returns>A collection of all suppliers.</returns>
        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            using (var db = _dapperDbConnection.CreateConnection())
            {
                return await db.QueryAsync<Supplier>("select * from Supplier order by SupplierName");
            }
        }
    }
}
