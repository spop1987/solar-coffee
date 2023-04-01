using Microsoft.AspNetCore.Mvc;
using SolarCoffee.Data.Dtos;
using SolarCoffee.Services.IServices;

namespace SolarCoffee.Web.Controllers
{
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("/api/inventory")]
        public ActionResult GetCurrentInventory()
        {
            return Ok(_inventoryService.GetCurrentInventory());
        }

        [HttpPatch("/api/inventory")]
        public ActionResult UpdateInventory([FromBody] ShipmentDto shipmentDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_inventoryService.UpdateUnitsAvailable(shipmentDto.ProductId, shipmentDto.Adjustment));
        }

        [HttpGet("/api/inventory/snapshot")]
        public ActionResult GetSnapshotHistory()
        {
            return Ok(_inventoryService.GetSnapshotHistory());
        }
    }
}