
using SolarCoffee.Data.Models;

namespace SolarCoffee.Data.DataAccess
{
    public interface ICommands
    {
        bool SaveChanges();
        int AddProduct(Product product);
    }

    public class Commands : ICommands
    {
        private readonly SolarDbContext _context;

        public Commands(SolarDbContext context)
        {
            _context = context;
        }

        public int AddProduct(Product product)
        {
            _context.Add(product);
            if(SaveChanges())
                return product.Id;
            
            return -1;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}