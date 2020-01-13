using Genesis.Core.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Genesis.Data.EntityFramework.Entities
{
    [Table("CompanyAddresses")]
    public class CompanyAddressEntity
    {

        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public bool IsEstablishement { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }

        public static CompanyAddressEntity MapFromBusinessObject(Address address, bool isEstablishement = false)
        {
            return new CompanyAddressEntity()
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                PostCode = address.PostCode,
                City = address.City,
                IsEstablishement = isEstablishement
            };
        }

        public Address MapToBusinessObject()
        {
            return new Address()
            {
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                PostCode = PostCode,
                City = City
            };
        }
    }
}
