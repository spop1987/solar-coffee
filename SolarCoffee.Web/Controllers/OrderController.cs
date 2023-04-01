using Microsoft.AspNetCore.Mvc;
using SolarCoffee.Data.Dtos;
using SolarCoffee.Services.IServices;

namespace SolarCoffee.Web.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController(IOrderService orderService, ICustomerService customerService)
        {
            _orderService = orderService;
            _customerService = customerService;
        }

        [HttpPost("/api/invoice")]
        public ActionResult GenerateNewOrder([FromBody] InvoiceDto invoiceDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var order = _orderService.GetSalesOrder(invoiceDto);
            order.Customer = _customerService.GetCustomerById(invoiceDto.CustomerId).Data;
            return Ok(_orderService.GenerateOpenOrder(order));
        }

        [HttpGet("/api/order")]
        public ActionResult GetOrders()
        {
            return Ok(_orderService.GetOrders());
        }

        [HttpPatch("/api/order/complete/{id}")]
        public ActionResult MarkOrderComplete(int id)
        {
            return Ok(_orderService.MarkFulfilled(id));
        }

    }
}