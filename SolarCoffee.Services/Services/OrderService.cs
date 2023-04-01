using SolarCoffee.Data.DataAccess;
using SolarCoffee.Data.Dtos;
using SolarCoffee.Data.Models;
using SolarCoffee.Data.Translators;
using SolarCoffee.Services.IServices;
using SolarCoffee.Services.ModelResponse;
using SolarCoffee.Services.Utils;

namespace SolarCoffee.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IToEntityTranslator _toEntityTranslator;
        private readonly IToDtoTranslator _toDtoTranslator;
        private readonly ICommands _commands;
        private readonly IQueries _queries;

        public OrderService(IToEntityTranslator toEntityTranslator,
                            IToDtoTranslator toDtoTranslator,
                            ICommands commands,
                            IQueries queries)
        {
            _toEntityTranslator = toEntityTranslator;
            _toDtoTranslator = toDtoTranslator;
            _commands = commands;
            _queries = queries;
        }
        public ServiceResponse<int> GenerateOpenOrder(SalesOrderDto salesOrderDto)
        {
            var salesOrder = _toEntityTranslator.ToSalesOrder(salesOrderDto);
            var salesOrderId = _commands.CreateSalesOrder(salesOrder);
            if(salesOrderId < 0)
                throw new Exception("Something went wrong creating SalesOrder");
            
            return UtilsResponse.GenericResponse<int>(salesOrderId, "Open Order Created", true);
        }

        public List<SalesOrderDto> GetOrders()
        {
            var listOfSalesOrder = _queries.GetAllSalesOrders();
            return _toDtoTranslator.ToListOfSalesOrderDto(listOfSalesOrder);
        }

        public SalesOrderDto GetSalesOrder(InvoiceDto invoiceDto)
        {
            var salesOrder = invoiceDto.LineItems.Select(item => new SalesOrderItemDto
            {
                Quantity = item.Quantity,
                Product = item.Product
            }).ToList();
            var salesOrderDto = new SalesOrderDto
            {
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                SalesOrderItems = salesOrder
            };
            return salesOrderDto;
        }

        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            var order = _queries.GetSalesOrderById(id);
            if(order is null)
                throw new Exception($"Order does not exists with id: {id}");
            order.UpdatedOn = DateTime.UtcNow;
            order.IsPaid = true;

            if(!_commands.UpdateOrder(order))
                throw new Exception($"Something went wrong trying to Mark fulfilled the order {id}");
            
            return UtilsResponse.GenericResponse<bool>(true, $"Order {order.Id} closed: Invoice paid is full.");
        }
    }
}