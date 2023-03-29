using SolarCoffee.Services.Services;
using SolarCoffee.Data.Models;
using SolarCoffee.Data.Dtos;

namespace SolarCoffee.Services.IServices
{
    public interface IProductService
    {
        List<ProductDto> GetAllProducts();
        ServiceResponse<ProductDto> GetProductById(int productId);
        ServiceResponse<int> CreateProduct(ProductDto product);
        ServiceResponse<ProductDto> ArchiveProduct(int productId); 
    }
}