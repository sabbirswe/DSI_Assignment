using DSI_Assignment.Application.DTO;
using DSI_Assignment.Application.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSI_Assignment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        [Route("get-suppliers")]
        public async Task<IActionResult> GetSuppliers()
        {
            var orders = await _supplierService.GetAllAsync();
            
            return Ok(orders);
        }

    }
}
