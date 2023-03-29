using System.Collections.Generic;
using SolarCoffee.Data.Dtos;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Data.Translators
{
    public interface IToDtoTranslator
    {
        ProductDto ToProductDto(Product product);
        List<ProductDto> ToListOfProductDto(List<Product> products);
    }

    public class ToDtoTranslator : IToDtoTranslator
    {
        public ProductDto ToProductDto(Product product)
        {
            return new ProductDto
            {
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsArchived = product.IsArchived,
                IsTaxable = product.IsTaxable
            };
        }

        public List<ProductDto> ToListOfProductDto(List<Product> products)
        {
            var listOfProductsDto = new List<ProductDto>();
            products.ForEach( p => {
                listOfProductsDto.Add(ToProductDto(p));
            });

            return listOfProductsDto;
        }
    }
}