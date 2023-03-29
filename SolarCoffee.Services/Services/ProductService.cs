using SolarCoffee.Data.Dtos;
using SolarCoffee.Services.IServices;
using SolarCoffee.Data.Translators;
using SolarCoffee.Data.DataAccess;
using SolarCoffee.Services.ModelResponse;

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
                return new ServiceResponse<ProductDto>
                {
                    Data = productDto,
                    Time = DateTime.UtcNow,
                    Message = "Archived Product",
                    IsSuccess = true,
                };
            
            return new ServiceResponse<ProductDto>
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = "Something went wrong",
                    IsSuccess = false,
                };
        }

        public ServiceResponse<int> CreateProduct(ProductDto productDto)
        {
            var product = _toEntityTranslator.ToProduct(productDto);
            var productId = _commands.AddProduct(product);
            if(productId > 0)
                return new ServiceResponse<int>
                {
                    Data = productId,
                    Time = DateTime.UtcNow,
                    Message = "Save new product",
                    IsSuccess = true
                };
            
            return new ServiceResponse<int>
            {
                Data = productId,
                Time = DateTime.UtcNow,
                Message = "Something unexpected happend",
                IsSuccess = false
            };
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
            if(productIdDb != null)
                return new ServiceResponse<ProductDto>
                {
                    Data = _toDtoTranslator.ToProductDto(productIdDb),
                    Time = DateTime.UtcNow,
                    Message = "Getting product",
                    IsSuccess = true
                };

            return new ServiceResponse<ProductDto>
            {
                Data = null,
                Time = DateTime.UtcNow,
                Message = "The product does not exists",
                IsSuccess = true
            }; 
        }
    }
}