using SolarCoffee.Data.Dtos;
using SolarCoffee.Services.ModelResponse;

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