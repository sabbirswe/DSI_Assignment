using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Assignment.Application
{
    public class PagedResponse<T>
    {
        public T Data { get; set; }
        public int TotalRows { get; set; }
    }
}
