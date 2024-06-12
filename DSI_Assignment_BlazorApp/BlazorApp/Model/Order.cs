using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Model
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "SUPPLIER is required")]
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string RefID { get; set; }

        [Required(ErrorMessage ="PO. NO is required")]
        public string PoNo { get; set; }

        public DateTime PoDate { get; set; }

        [Required(ErrorMessage = "EXPECTED DATE is required")]
        public DateTime? ExpectedDate { get; set; }

        public string Remarks { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public bool IsOrderDetailsModified { get; set; }
    }
}
