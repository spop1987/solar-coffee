using SolarCoffee.Data.Dtos;
using SolarCoffee.Services.ModelResponse;

namespace SolarCoffee.Services.IServices
{
    public interface IOrderService
    {
        List<SalesOrderDto> GetOrders();
        ServiceResponse<int> GenerateOpenOrder(SalesOrderDto orderDto);
        ServiceResponse<bool> MarkFulfilled(int id);
    }
}