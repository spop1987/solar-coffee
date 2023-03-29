using System;
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

        public CustomerDto ToCustomerDto(Customer customer)
        {
            return new CustomerDto
            {
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = ToPrimaryAddressDto(customer.PrimaryAddress)
            };
        }

        public ProductInventoryDto ToProductInventoryDto(ProductInventory productInventory)
        {
            return new ProductInventoryDto
            {
                CreatedOn = productInventory.CreatedOn,
                UpdatedOn = productInventory.UpdatedOn,
                IdealQuantity = productInventory.IdealQuantity,
                QuantityOnHand = productInventory.QuantityOnHand,
                Product = ToProductDto(productInventory.Product)
            };
        }

        public ProductInventorySnapshotDto ToProductInventorySnapshotDto(ProductInventorySnapshot productInventorySnapshot)
        {
            return new ProductInventorySnapshotDto
            {
                SnapshotTime = productInventorySnapshot.SnapshotTime,
                QuantityOnHand = productInventorySnapshot.QuantityOnHand,
                Product = ToProductDto(productInventorySnapshot.Product)
            };
        }

        public SalesOrderDto ToSalesOrderDto(SalesOrder salesOrder)
        {
            return new SalesOrderDto
            {
                CreatedOn = salesOrder.CreatedOn,
                UpdatedOn = salesOrder.UpdatedOn,
                IsPaid = salesOrder.IsPaid,
                Customer = ToCustomerDto(salesOrder.Customer),
                SalesOrderItems = ToSalesOrderItemsDto(salesOrder.SalesOrderItems)
            };
        }

        public SalesOrderItemDto ToSalesOrderItemDto(SalesOrderItem salesOrderItem)
        {
            return new SalesOrderItemDto
            {
                Quantity = salesOrderItem.Quantity,
                Product = ToProductDto(salesOrderItem.Product)
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

        private List<SalesOrderItemDto> ToSalesOrderItemsDto(List<SalesOrderItem> salesOrderItems)
        {
            var listOfSalesOrderItem = new List<SalesOrderItemDto>();
            if(salesOrderItems.Count == 0)
                return listOfSalesOrderItem;
            
            salesOrderItems.ForEach(s => {
                listOfSalesOrderItem.Add(ToSalesOrderItemDto(s));
            });

            return listOfSalesOrderItem;
        }

        private CustomerAddressDto ToPrimaryAddressDto(CustomerAddress primaryAddress)
        {
            return new CustomerAddressDto
            {
                CreatedOn = primaryAddress.CreatedOn,
                UpdatedOn = primaryAddress.UpdatedOn,
                AddressLine1 = primaryAddress.AddressLine1,
                AddressLine2 = primaryAddress.AddressLine2,
                City = primaryAddress.City,
                Country = primaryAddress.Country,
                PostalCode = primaryAddress.PostalCode,
                State = primaryAddress.State
            };
        }
    }
}