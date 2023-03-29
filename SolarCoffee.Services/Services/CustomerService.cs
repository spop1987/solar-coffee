using SolarCoffee.Data.Dtos;
using SolarCoffee.Services.IServices;
using SolarCoffee.Services.ModelResponse;

namespace SolarCoffee.Services.Services
{
    public class CustomerService : ICustomerService
    {
        public ServiceResponse<int> CreateCustomer(CustomerDto customerDto)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<bool> DeleteCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public List<CustomerDto> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public CustomerDto GetCustomerById(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}