using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Assignment.Infrastructure
{
    public interface IDapperDbConnection
    {
        IDbConnection CreateConnection();
    }
}
