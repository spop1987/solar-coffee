using System.Collections.Generic;
using System.Linq;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Data.DataAccess
{
    public interface IQueries
    {
        List<Product> GetAllProducts();
        Product GetProductById(int productId);
    }
    public class Queries : IQueries
    {
        private readonly SolarDbContext _context;

        public Queries(SolarDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            return ProductQuery().OrderBy(p => p.Name).ToList();
        }

        public Product GetProductById(int productId)
        {
            return ProductQuery().FirstOrDefault(p => p.Id == productId);
        }

        private IQueryable<Product> ProductQuery()
        {
            return _context.Products.Where(p => !string.IsNullOrEmpty(p.Name) && !string.IsNullOrEmpty(p.Description)).AsQueryable();
        }
    }
}