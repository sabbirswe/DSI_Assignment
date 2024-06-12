using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Assignment.Application.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }

        public string RefID { get; set; }

        public string PoNo { get; set; }

        public DateTime PoDate { get; set; }

        public DateTime? ExpectedDate { get; set; }

        public string? Remarks { get; set; }

        public IEnumerable<OrderDetailDto> OrderDetails { get; set; }
        public bool IsOrderDetailsModified { get; set; }
    }
}
