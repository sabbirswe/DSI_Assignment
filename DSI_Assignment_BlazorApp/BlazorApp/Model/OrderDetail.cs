namespace BlazorApp.Model
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int OrderId { get; set; }
        public int? Qty { get; set; }
        public double? Rate { get; set; }

    }
}
