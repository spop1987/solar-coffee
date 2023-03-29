using SolarCoffee.Data.Dtos;
using SolarCoffee.Services.IServices;
using SolarCoffee.Services.ModelResponse;

namespace SolarCoffee.Services.Services
{
    public class OrderService : IOrderService
    {
        public ServiceResponse<bool> GenerateOpenOrder(SalesOrderDto orderDto)
        {
            throw new NotImplementedException();
        }

        public List<SalesOrderDto> GetOrders()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            throw new NotImplementedException();
        }
    }
}