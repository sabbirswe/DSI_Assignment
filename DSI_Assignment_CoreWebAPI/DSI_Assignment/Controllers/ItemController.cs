using DSI_Assignment.Application.DTO;
using DSI_Assignment.Application.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSI_Assignment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        [Route("get-items")]
        public async Task<IActionResult> GetItems()
        {
            var orders = await _itemService.GetAllAsync();

            return Ok(orders);
        }
    }
}
