using System;
using Microsoft.AspNetCore.Mvc;
using SolarCoffee.Data.Dtos;
using SolarCoffee.Services.IServices;

namespace SolarCoffee.Web.Controllers
{
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("/api/customer")]
        public ActionResult CreateCustomer([FromBody] CustomerDto customerDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            customerDto.CreatedOn = DateTime.UtcNow;
            customerDto.UpdatedOn = DateTime.UtcNow;
            return Ok(_customerService.CreateCustomer(customerDto));
        }

        [HttpGet("/api/customer")]
        public ActionResult GetCustomer()
        {
            return Ok(_customerService.GetAllCustomers());
        }

        [HttpDelete("/api/customer")]
        public ActionResult DeleteCustomer(int customerId)
        {
            return Ok(_customerService.DeleteCustomer(customerId));
        }
    }
}