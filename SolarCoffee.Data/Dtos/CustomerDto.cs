using System;

namespace SolarCoffee.Data.Dtos
{
    public class CustomerDto
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public CustomerAddressDto PrimaryAddress { get; set; }
    }
}