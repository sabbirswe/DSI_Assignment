using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSI_Assignment.Application.DTO
{
    public class ItemDto
    {
        public int ItemId { get; set; }

        [Required]
        [StringLength(150)]
        public string ItemName { get; set; }

    }
}
