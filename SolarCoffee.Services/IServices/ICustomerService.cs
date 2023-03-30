using SolarCoffee.Data.Dtos;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.ModelResponse;

namespace SolarCoffee.Services.IServices
{
    public interface ICustomerService
    {
        List<CustomerDto> GetAllCustomers();
        ServiceResponse<int> CreateCustomer(CustomerDto customerDto);
        ServiceResponse<int> DeleteCustomer(int customerId);
        ServiceResponse<CustomerDto> GetCustomerById(int customerId);
    }
}