using DSI_Assignment.Application.DTO;
using DSI_Assignment.Application.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelectPdf;
using System;

namespace DSI_Assignment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ISupplierService _supplierService;
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        public OrderController(IOrderService orderService, IOrderDetailService orderDetailService, IItemService itemService, ISupplierService supplierService)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _itemService = itemService;
            _supplierService = supplierService;
        }

        [HttpGet]
        [Route("get-orders")]
        public async Task<IActionResult> GetOrders(int pageSize, int pageNumber, string? queryString)
        {
            var orders = await _orderService.GetAll(pageSize, pageNumber, queryString);
            
            return Ok(orders);
        }

        [HttpGet]
        [Route("get-order/{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            order.OrderDetails = await _orderDetailService.GetAllByOrderIdAsync(id);

            return Ok(order);
        }

        [HttpGet]
        [Route("generate-refno")]
        public async Task<IActionResult> GenerateRefNo()
        {
            return Ok(await _orderService.GenerateOrderRefNoAsync());
        }

        [HttpGet]
        [Route("generate-order-report/{id}")]
        public async Task<IActionResult> GenerateOrderReport(int id)
        {
            HtmlToPdf htmlToPdf = new HtmlToPdf();
            htmlToPdf.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            // put css in pdf
            htmlToPdf.Options.MarginLeft = 25;
            htmlToPdf.Options.MarginRight = 25;
            var orderInfo = await _orderService.GetByIdAsync(id);
            var orderDetails = await _orderDetailService.GetAllByOrderIdAsync(id);
            string htmlContent = _orderService.GenerateOrderReportHtml(orderInfo, orderDetails);
            var fileName = "Order_" + orderInfo.RefID + "_" + orderInfo.PoNo;
            PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(htmlContent);
            byte[] pdf = pdfDocument.Save();
            //convert to memory stream
            Stream stream = new MemoryStream(pdf);
            pdfDocument.Close();
            //if want to transfer stream to file 
            return File(stream, "application/pdf",  fileName+ ".pdf");
        }

        [HttpPost]
        [Route("create-order")]
        public async Task<IActionResult> CreateOrder(OrderDto model)
        {
            await _orderService.CreateAsync(model);
            return Ok();
        }

        [HttpPut]
        [Route("update-order/{id}")]
        public async Task<IActionResult> UpdateOrder(int id,OrderDto model)
        {
            await _orderService.UpdateAsync(id,model);
            return Ok();
        }

        [HttpDelete]
        [Route("delete-order/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteAsync(id);
            return Ok();
        }

    }
}
