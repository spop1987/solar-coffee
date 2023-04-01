using SolarCoffee.Data.DataAccess;
using SolarCoffee.Data.Dtos;
using SolarCoffee.Data.Translators;
using SolarCoffee.Services.IServices;
using SolarCoffee.Services.ModelResponse;
using SolarCoffee.Services.Utils;

namespace SolarCoffee.Services.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IToEntityTranslator _toEntityTranslator;
        private readonly IToDtoTranslator _toDtoTranslator;
        private readonly ICommands _commands;
        private readonly IQueries _queries;

        public InventoryService(IToEntityTranslator toEntityTranslator,
                               IToDtoTranslator toDtoTranslator,
                               ICommands commands,
                               IQueries queries)
        {
            _toEntityTranslator = toEntityTranslator;
            _toDtoTranslator = toDtoTranslator;
            _commands = commands;
            _queries = queries;
        }
        public ProductInventoryDto GetByProductId(int productId)
        {
            var productInventory = _queries.GetPIByProductId(productId);
            if(productInventory == null)
                throw new Exception("ProductInventory does not exists!");
            
            return _toDtoTranslator.ToProductInventoryDto(productInventory);
        }

        public List<ProductInventoryDto> GetCurrentInventory()
        {
            var listOfProductInventory = _queries.GetCurrentInventory();
            if(listOfProductInventory.Count > 0)
                return _toDtoTranslator.ToListOfProductInventoryDto(listOfProductInventory);
            
            return new List<ProductInventoryDto>();
        }

        public SnapshotResponseDto GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(2);
            var listOfProductInventorySnaapshot = _queries.GetSnapshotHistory(earliest);
            if(listOfProductInventorySnaapshot.Count == 0)
                throw new Exception("There are not snapshot history");

            var timelineMarkers = listOfProductInventorySnaapshot.Select(t => t.SnapshotTime).Distinct().ToList();
            var snapshots = listOfProductInventorySnaapshot
                .GroupBy(hist => hist.Product, 
                        hist => hist.QuantityOnHand, 
                        (key, g) => new SnapshotHistoryDto
                                    {   ProductId = key.Id, 
                                        QuantityOnHand = g.ToList()
                                    }).OrderBy(hist => hist.ProductId).ToList();
            
            var snapshotResponseDto = new SnapshotResponseDto
            {
                Timeline = timelineMarkers,
                ListOfSnapshotHistoryDto = snapshots
            };
            return snapshotResponseDto;
        }

        public ServiceResponse<ProductInventoryDto> UpdateUnitsAvailable(int id, int adjustment)
        {
            var now = DateTime.UtcNow;

            var inventory = _queries.GetPIByProductId(id);
            if(inventory is null)
                throw new Exception($"Inventory does not exists for the ProductId: {id}");
            
            inventory.QuantityOnHand += adjustment;

            CreateSnapshot();

            if(!_commands.SaveChanges())
                throw new Exception($"Something went wrong trying to save Inventory: {inventory.Id}");
            
            return UtilsResponse.GenericResponse<ProductInventoryDto>(_toDtoTranslator.ToProductInventoryDto(inventory), $"Product {id} inventory adjusted", true);
        }

        private void CreateSnapshot()
        {
            var now = DateTime.UtcNow;

            var inventories = _queries.GetAllProductInventory();

            foreach (var inventory in inventories)
            {
                if(!_commands.CreateProductInventorySnapshot(inventory))
                    throw new Exception("Something went wrong trying to create a ProductInventorySnapshot!");
            }
        }
    }
}