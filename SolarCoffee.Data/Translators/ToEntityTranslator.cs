using System;
using System.Collections.Generic;
using SolarCoffee.Data.Dtos;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Data.Translators
{
    public interface IToEntityTranslator
    {
        Product ToProduct(ProductDto productDto, Product product = null);
        public Customer ToCustomer(CustomerDto customerDto, Customer customer = null);
        ProductInventory ToProductInventory(ProductInventoryDto productInventoryDto, ProductInventory productInventory = null);
        ProductInventorySnapshot ToProductInventorySnapshot(ProductInventorySnapshotDto productInventorySnapshotDto, ProductInventorySnapshot productInventorySnapshot = null);
        SalesOrder ToSalesOrder(SalesOrderDto salesOrderDto, SalesOrder salesOrder = null);
        SalesOrderItem ToSalesOrderItem(SalesOrderItemDto salesOrderItemDto, SalesOrderItem salesOrderItem = null);
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

        public Customer ToCustomer(CustomerDto customerDto, Customer customer = null)
        {
            if(customer == null)
                return new Customer{
                    CreatedOn = customerDto.CreatedOn,
                    UpdatedOn = customerDto.UpdatedOn,
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                    PrimaryAddress = ToPrimaryAddress(customerDto.PrimaryAddress)
                };
            
            customer.CreatedOn = customerDto.CreatedOn;
            customer.UpdatedOn = customerDto.UpdatedOn;
            customer.FirstName = customerDto.FirstName;
            customer.LastName = customerDto.LastName;
            customer.PrimaryAddress = ToPrimaryAddress(customerDto.PrimaryAddress);
            return customer;
        }

        public ProductInventory ToProductInventory(ProductInventoryDto productInventoryDto, ProductInventory productInventory = null)
        {
            if(productInventory == null)
                return new ProductInventory{
                    CreatedOn = productInventoryDto.CreatedOn,
                    UpdatedOn = productInventoryDto.UpdatedOn,
                    IdealQuantity = productInventoryDto.IdealQuantity,
                    QuantityOnHand = productInventoryDto.QuantityOnHand,
                    Product = ToProduct(productInventoryDto.Product)
                };
            
            productInventory.CreatedOn = productInventoryDto.CreatedOn;
            productInventory.UpdatedOn = productInventoryDto.UpdatedOn;
            productInventory.IdealQuantity = productInventoryDto.IdealQuantity;
            productInventory.QuantityOnHand = productInventoryDto.QuantityOnHand;
            productInventory.Product = ToProduct(productInventoryDto.Product);
            return productInventory;
        }

        public ProductInventorySnapshot ToProductInventorySnapshot(ProductInventorySnapshotDto productInventorySnapshotDto, ProductInventorySnapshot productInventorySnapshot = null)
        {
            if(productInventorySnapshot == null)
                return new ProductInventorySnapshot
                {
                    SnapshotTime = productInventorySnapshotDto.SnapshotTime,
                    QuantityOnHand = productInventorySnapshotDto.QuantityOnHand,
                    Product = ToProduct(productInventorySnapshotDto.Product)
                };
            
            productInventorySnapshot.SnapshotTime = productInventorySnapshotDto.SnapshotTime;
            productInventorySnapshot.QuantityOnHand = productInventorySnapshotDto.QuantityOnHand;
            productInventorySnapshot.Product = ToProduct(productInventorySnapshotDto.Product);
            return productInventorySnapshot;
        }

        public SalesOrder ToSalesOrder(SalesOrderDto salesOrderDto, SalesOrder salesOrder = null)
        {
            if(salesOrder == null)
                return new SalesOrder
                {
                    CreatedOn = salesOrderDto.CreatedOn,
                    UpdatedOn = salesOrderDto.UpdatedOn,
                    IsPaid = salesOrderDto.IsPaid,
                    Customer = ToCustomer(salesOrderDto.Customer),
                    SalesOrderItems = ToSalesOrderItems(salesOrderDto.SalesOrderItems)
                };
            
            salesOrder.CreatedOn = salesOrderDto.CreatedOn;
            salesOrder.UpdatedOn = salesOrderDto.UpdatedOn;
            salesOrder.IsPaid = salesOrderDto.IsPaid;
            salesOrder.Customer = ToCustomer(salesOrderDto.Customer);
            salesOrder.SalesOrderItems = ToSalesOrderItems(salesOrderDto.SalesOrderItems);
            return salesOrder;
        }

        public SalesOrderItem ToSalesOrderItem(SalesOrderItemDto salesOrderItemDto, SalesOrderItem salesOrderItem = null)
        {
            if(salesOrderItem == null)
                return new SalesOrderItem
                {
                    Quantity = salesOrderItemDto.Quantity,
                    Product = ToProduct(salesOrderItemDto.Product)
                };
            
            salesOrderItem.Quantity = salesOrderItemDto.Quantity;
            salesOrderItem.Product = ToProduct(salesOrderItemDto.Product);
            return salesOrderItem;
        }

        private List<SalesOrderItem> ToSalesOrderItems(List<SalesOrderItemDto> salesOrderItems)
        {
            var listOfSalesOrderItem = new List<SalesOrderItem>();
            if(salesOrderItems.Count == 0)
                return listOfSalesOrderItem;
            
            salesOrderItems.ForEach(s => {
                listOfSalesOrderItem.Add(ToSalesOrderItem(s));
            });

            return listOfSalesOrderItem;
        }

        private CustomerAddress ToPrimaryAddress(CustomerAddressDto primaryAddressDto, CustomerAddress primaryAddress = null)
        {
            if(primaryAddress == null)
                return new CustomerAddress
                {
                    CreatedOn = primaryAddressDto.CreatedOn,
                    UpdatedOn = primaryAddressDto.UpdatedOn,
                    AddressLine1 = primaryAddressDto.AddressLine1,
                    AddressLine2 = primaryAddressDto.AddressLine2,
                    City = primaryAddressDto.City,
                    Country = primaryAddressDto.Country,
                    PostalCode = primaryAddressDto.PostalCode,
                    State = primaryAddressDto.State
                };
            
            primaryAddress.CreatedOn = primaryAddressDto.CreatedOn;
            primaryAddress.UpdatedOn = primaryAddressDto.UpdatedOn;
            primaryAddress.AddressLine1 = primaryAddressDto.AddressLine1;
            primaryAddress.AddressLine2 = primaryAddressDto.AddressLine2;
            primaryAddress.City = primaryAddressDto.City;
            primaryAddress.Country = primaryAddressDto.Country;
            primaryAddress.PostalCode = primaryAddressDto.PostalCode;
            primaryAddress.State = primaryAddressDto.State;
            return primaryAddress;
        }
    }
}