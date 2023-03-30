using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data.Dtos;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Data.DataAccess
{
    public interface IQueries
    {
        List<Product> GetAllProducts();
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int customerId);
        Product GetProductById(int productId);
        List<ProductInventory> GetCurrentInventory();
        ProductInventory GetPIByProductId(int productId);
        List<ProductInventorySnapshot> GetSnapshotHistory(DateTime earliest);
        List<ProductInventory> GetAllProductInventory();
        List<SalesOrder> GetAllSalesOrders();
        SalesOrder GetSalesOrderById(int id);
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

        public List<Customer> GetAllCustomers()
        {
            return CustomerQuery().OrderBy(c => c.FirstName).ToList();
        }

        public List<ProductInventory> GetCurrentInventory()
        {
            return ProductInventoryQuery().Where(pi => !pi.Product.IsArchived).ToList();
        }

        public List<ProductInventory> GetAllProductInventory()
        {
            return ProductInventoryQuery().ToList();
        }

        public Customer GetCustomerById(int customerId)
        {
            return CustomerQuery().FirstOrDefault(c => c.Id == customerId);
        }

        public Product GetProductById(int productId)
        {
            return ProductQuery().FirstOrDefault(p => p.Id == productId);
        }

        public ProductInventory GetPIByProductId(int productId)
        {
            return ProductInventoryQuery().FirstOrDefault(pi => pi.Product.Id == productId);
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory(DateTime earliest)
        {
            return ProductInventorySnapshotQuery().Where(snap => snap.SnapshotTime > earliest && !snap.Product.IsArchived).ToList();
        }

        public List<SalesOrder> GetAllSalesOrders()
        {
            return SalesOrderQuery().ToList();
        }

        public SalesOrder GetSalesOrderById(int id)
        {
            return SalesOrderQuery().FirstOrDefault(so => so.Id == id);
        }

        private IQueryable<Product> ProductQuery()
        {
            return _context.Products.Where(p => !string.IsNullOrEmpty(p.Name) && !string.IsNullOrEmpty(p.Description)).AsQueryable();
        }

        private IQueryable<Customer> CustomerQuery()
        {
            return _context.Customers.Include(c => c.PrimaryAddress)
                        .Where(c => !string.IsNullOrEmpty(c.FirstName) && !string.IsNullOrEmpty(c.LastName)).AsQueryable();
        }

        private IQueryable<ProductInventory> ProductInventoryQuery()
        {
            return _context.ProductInventories.Include(pi => pi.Product).AsQueryable();
        }

        private IQueryable<ProductInventorySnapshot> ProductInventorySnapshotQuery()
        {
            return _context.ProductInventorySnapshots.Include(snap => snap.Product).AsQueryable();
        }

        private IQueryable<SalesOrder> SalesOrderQuery()
        {
            return _context.SalesOrders.Include(so => so.Customer)
                .Include(so => so.SalesOrderItems).ThenInclude(soi => soi.Product).AsQueryable();
        }
    }
}