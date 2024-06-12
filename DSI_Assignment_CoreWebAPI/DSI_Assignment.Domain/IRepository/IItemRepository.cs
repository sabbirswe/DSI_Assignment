using DSI_Assignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Assignment.Domain.IRepository
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();
    }
}
