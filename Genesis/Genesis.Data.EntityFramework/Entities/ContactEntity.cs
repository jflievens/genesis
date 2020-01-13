using Genesis.Core.Enums;
using Genesis.Core.Models;
using System;

namespace Genesis.Data.EntityFramework.Entities
{
    public class ContactEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ContactType Type { get; set; }
        public string VatNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }


        public static ContactEntity MapFromBusinessObject(Contact contact)
        {
            return new ContactEntity()
            {
                Id = contact.Id,
                Name = contact.Name,
                Type = contact.Type,
                VatNumber = contact.VatNumber,
                AddressLine1 = contact.Address?.AddressLine1,
                AddressLine2 = contact.Address?.AddressLine2,
                PostCode = contact.Address?.PostCode,
                City = contact.Address?.City
            };
        }

        public Contact MapToBusinessObject()
        {
            return new Contact()
            {
                Id = Id,
                Name = Name,
                Type = Type,
                VatNumber = VatNumber,
                Address = new Address()
                {
                    AddressLine1 = AddressLine1,
                    AddressLine2 = AddressLine2,
                    PostCode = PostCode,
                    City = City
                }
            };
        }
    }

}
