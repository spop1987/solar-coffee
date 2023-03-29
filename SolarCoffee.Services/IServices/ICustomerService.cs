using SolarCoffee.Data.Dtos;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.ModelResponse;

namespace SolarCoffee.Services.IServices
{
    public interface ICustomerService
    {
        List<CustomerDto> GetAllCustomers();
        ServiceResponse<int> CreateCustomer(CustomerDto customerDto);
        ServiceResponse<bool> DeleteCustomer(int customerId);
        CustomerDto GetCustomerById(int customerId);
    }
}