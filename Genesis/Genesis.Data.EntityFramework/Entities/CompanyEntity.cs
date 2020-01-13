using Genesis.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Genesis.Data.EntityFramework.Entities
{
    public class CompanyEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string VatNumber { get; set; }

        [ForeignKey("CompanyId")]
        public ICollection<CompanyAddressEntity> Addresses { get; set; }

        public static CompanyEntity MapFromBusinessObject(Company company)
        {
            var companyEntity = new CompanyEntity()
            {
                Id = company.Id,
                Name = company.Name,
                VatNumber = company.VatNumber
            };
            companyEntity.Addresses = new List<CompanyAddressEntity>();

            var establishmentAddressEntity = CompanyAddressEntity.MapFromBusinessObject(company.EstablishmentAddress, true);
            companyEntity.Addresses.Add(establishmentAddressEntity);

            if (company.SubsidiaryAddresses != null)
            {
                foreach (var subsidiaryAddress in company.SubsidiaryAddresses)
                {
                    var subsidiaryAddressEntity = CompanyAddressEntity.MapFromBusinessObject(subsidiaryAddress, false);
                    companyEntity.Addresses.Add(subsidiaryAddressEntity);
                }
            }

            return companyEntity;
        }


        public Company MapToBusinessObject()
        {
            var company = new Company
            {
                Id = Id,
                Name = Name,
                VatNumber = VatNumber
            };

            if (Addresses != null)
            {
                var establishmentAddressEntity = Addresses.FirstOrDefault(a => a.IsEstablishement);
                if (establishmentAddressEntity != null)
                {
                    company.EstablishmentAddress = establishmentAddressEntity.MapToBusinessObject();
                }

                company.SubsidiaryAddresses = new List<Address>();
                foreach (var subsidiaryAddressEntity in Addresses.Where(a => !a.IsEstablishement))
                {
                    company.SubsidiaryAddresses.Add(subsidiaryAddressEntity.MapToBusinessObject());
                }
            }

            return company;
        }
    }
}
