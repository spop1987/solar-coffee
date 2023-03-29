using SolarCoffee.Data.Dtos;
using SolarCoffee.Services.IServices;
using SolarCoffee.Services.ModelResponse;

namespace SolarCoffee.Services.Services
{
    public class InventoryService : IInventoryService
    {
        public ProductInventoryDto GetByProductId(int productId)
        {
            throw new NotImplementedException();
        }

        public List<ProductInventoryDto> GetCurrentInventory()
        {
            throw new NotImplementedException();
        }

        public List<ProductInventorySnapshotDto> GetSnapshotHistory()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<ProductInventoryDto> UpdateUnitsAvailable(int id, int adjustment)
        {
            throw new NotImplementedException();
        }
    }
}