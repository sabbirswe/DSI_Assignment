using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Model
{
    public class Item
    {
        public int ItemId { get; set; }

        [Required]
        [StringLength(150)]
        public string ItemName { get; set; }
    }
}
