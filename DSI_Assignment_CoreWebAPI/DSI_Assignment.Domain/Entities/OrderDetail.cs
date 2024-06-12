using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Assignment.Domain.Entities
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int OrderId { get; set; }
        public int Qty { get; set; }
        public double Rate { get; set; }
        public Item Item { get; set; }
        public Order Order { get; set; }
    }
}
