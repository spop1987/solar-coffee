using SolarCoffee.Data.Dtos;
using SolarCoffee.Services.IServices;
using SolarCoffee.Data.Translators;
using SolarCoffee.Data.DataAccess;
using SolarCoffee.Services.ModelResponse;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Utils;

namespace SolarCoffee.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IToEntityTranslator _toEntityTranslator;
        private readonly IToDtoTranslator _toDtoTranslator;
        private readonly ICommands _commands;
        private readonly IQueries _queries;

        public ProductService(IToEntityTranslator toEntityTranslator, IToDtoTranslator toDtoTranslator, ICommands commands, IQueries queries)
        {
            _toEntityTranslator = toEntityTranslator;
            _toDtoTranslator = toDtoTranslator;
            _commands = commands;
            _queries = queries;
        }

        public ServiceResponse<ProductDto> ArchiveProduct(int productId)
        {
            var product = _queries.GetProductById(productId);
            var productDto = _toDtoTranslator.ToProductDto(product);
            productDto.IsArchived = true;
            _ = _toEntityTranslator.ToProduct(productDto, product);
            if(_commands.SaveChanges())
                return UtilsResponse.GenericResponse<ProductDto>(productDto, "Archived Product", true);
                
            throw new Exception("Something went wrong trying to archive product");
        }

        public ServiceResponse<int> CreateProduct(ProductDto productDto)
        {
            var product = _toEntityTranslator.ToProduct(productDto);
            var productId = _commands.AddProduct(product);
            if(productId > 0)
                return UtilsResponse.GenericResponse<int>(productId, "Save new product", true);
                
            return UtilsResponse.GenericResponse<int>(productId, "Something unexpected happend");
        }

        public List<ProductDto> GetAllProducts()
        {
            var listOfProducts = _queries.GetAllProducts();
            if(listOfProducts.Count > 0)
                return _toDtoTranslator.ToListOfProductDto(listOfProducts);
            
            return new List<ProductDto>();
        }

        public ServiceResponse<ProductDto> GetProductById(int productId)
        {
            var productIdDb = _queries.GetProductById(productId);
            if(productIdDb is null)
                throw new Exception("Product does not exists!");
                
            return UtilsResponse.GenericResponse<ProductDto>(_toDtoTranslator.ToProductDto(productIdDb), "Getting product", true);
        }
    }
}