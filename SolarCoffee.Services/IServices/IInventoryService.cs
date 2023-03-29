using SolarCoffee.Data.Dtos;
using SolarCoffee.Services.ModelResponse;

namespace SolarCoffee.Services.IServices
{
    public interface IInventoryService
    {
        public List<ProductInventoryDto> GetCurrentInventory();
        public ServiceResponse<ProductInventoryDto> UpdateUnitsAvailable(int id, int adjustment);
        public ProductInventoryDto GetByProductId(int productId);
        public List<ProductInventorySnapshotDto> GetSnapshotHistory();
    }
}