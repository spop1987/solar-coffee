using SolarCoffee.Data.DataAccess;
using SolarCoffee.Data.Dtos;
using SolarCoffee.Data.Translators;
using SolarCoffee.Services.IServices;
using SolarCoffee.Services.ModelResponse;
using SolarCoffee.Services.Utils;

namespace SolarCoffee.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IToEntityTranslator _toEntityTranslator;
        private readonly IToDtoTranslator _toDtoTranslator;
        private readonly ICommands _commands;
        private readonly IQueries _queries;

        public CustomerService(IToEntityTranslator toEntityTranslator,
                               IToDtoTranslator toDtoTranslator,
                               ICommands commands,
                               IQueries queries)
        {
            _toEntityTranslator = toEntityTranslator;
            _toDtoTranslator = toDtoTranslator;
            _commands = commands;
            _queries = queries;
        }
        public ServiceResponse<int> CreateCustomer(CustomerDto customerDto)
        {
            var customer = _toEntityTranslator.ToCustomer(customerDto);
            var customerId = _commands.AddCustomer(customer);

            if(customerId > 0)
                return UtilsResponse.GenericResponse<int>(customerId, "Save new customer", true);
            
            return UtilsResponse.GenericResponse<int>(customerId, "Something unexpected happend");
        }

        public ServiceResponse<int> DeleteCustomer(int customerId)
        {
            var customer = _queries.GetCustomerById(customerId);
            if(customer == null)
                return UtilsResponse.GenericResponse<int>(customerId, "Customer to delete not found");
            
            var response = _commands.DeleteCustomer(customer);
            if(response > 1)
                return UtilsResponse.GenericResponse<int>(response, "Customer deleted", true);
            
            return UtilsResponse.GenericResponse<int>(response, "Something unexpected happend");
        }

        public List<CustomerDto> GetAllCustomers()
        {
            var listOfCustomers = _queries.GetAllCustomers();
            if(listOfCustomers.Count > 0)
                return _toDtoTranslator.ToListOfCustomerDto(listOfCustomers);
            
            return new List<CustomerDto>();
        }

        public ServiceResponse<CustomerDto> GetCustomerById(int customerId)
        {
            var customerInDb = _queries.GetCustomerById(customerId);
            if(customerInDb is null)
                throw new Exception("Customer does not exists!");
            
            return UtilsResponse.GenericResponse<CustomerDto>(_toDtoTranslator.ToCustomerDto(customerInDb), "Getting Customer", true);
        }
    }
}