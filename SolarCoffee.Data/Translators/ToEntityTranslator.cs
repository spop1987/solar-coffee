using SolarCoffee.Data.Dtos;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Data.Translators
{
    public interface IToEntityTranslator
    {
        Product ToProduct(ProductDto productDto, Product product = null);
    }
    public class ToEntityTranslator : IToEntityTranslator
    {
        public Product ToProduct(ProductDto productDto, Product product = null)
        {
            if(product == null)
                return new Product{
                    CreatedOn = productDto.CreatedOn,
                    UpdatedOn = productDto.UpdatedOn,
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    IsTaxable = productDto.IsTaxable,
                    IsArchived = productDto.IsArchived
                };
            
            product.CreatedOn = productDto.CreatedOn;
            product.UpdatedOn = productDto.UpdatedOn;
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.IsTaxable = productDto.IsTaxable;
            product.IsArchived = productDto.IsArchived;
            return product;
        }
    }
}