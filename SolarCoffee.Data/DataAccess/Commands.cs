
using System;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Data.DataAccess
{
    public interface ICommands
    {
        bool SaveChanges();
        int AddProduct(Product product);
        int AddCustomer(Customer customer);
        int DeleteCustomer(Customer customer);
        bool CreateProductInventorySnapshot(ProductInventory productInventory);
        int CreateSalesOrder(SalesOrder salesOrder);
        bool UpdateOrder(SalesOrder order);
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
            _context.Products.Add(product);
            if(SaveChanges())
                return product.Id;
            
            return -1;
        }

        public int AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            if(SaveChanges())
                return customer.Id;
            
            return -1;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public int DeleteCustomer(Customer customer)
        {
            var customerId = customer.Id;
            _context.Customers.Remove(customer);
            if(SaveChanges())
                return customerId;
            
            return -1;
        }

        public bool CreateProductInventorySnapshot(ProductInventory productInventory)
        {
            var snapshot = new ProductInventorySnapshot
            {
                SnapshotTime = DateTime.UtcNow,
                Product = productInventory.Product,
                QuantityOnHand = productInventory.QuantityOnHand
            };
            _context.ProductInventorySnapshots.Add(snapshot);
            return (_context.SaveChanges() >= 0);
        }

        public int CreateSalesOrder(SalesOrder salesOrder)
        {
            _context.SalesOrders.Add(salesOrder);
            if(SaveChanges())
                return salesOrder.Id;
            
            return -1;
        }

        public bool UpdateOrder(SalesOrder order)
        {
            _context.SalesOrders.Update(order);
            return _context.SaveChanges() >= 0;
        }
    }
}