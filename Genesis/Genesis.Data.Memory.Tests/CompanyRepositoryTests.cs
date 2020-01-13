using Genesis.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Genesis.Data.Memory.Tests
{
    public class CompanyRepositoryTests
    {
        private readonly Storage _storage;
        private readonly CompanyRepository _repository;
        public CompanyRepositoryTests()
        {
            _storage = BuildStorage();
            _repository = new CompanyRepository(_storage);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnIEnumarbleOfCompanies()
        {
            var result = await _repository.GetAllAsync();

            Assert.IsAssignableFrom<IEnumerable<Company>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedCompany()
        {
            var company = new Company()
            {
                Name = "New created company",
                VatNumber = "BE.0123.456.789",
                EstablishmentAddress = new Address()
                {
                    AddressLine1 = "AddressLine1",
                    AddressLine2 = "AddressLine2",
                    PostCode = "PostCode",
                    City = "City"
                }
            };

            Company result = null;

            try
            {
                result = await _repository.CreateAsync(company);

                Assert.IsType<Company>(result);
                Assert.NotEqual(Guid.Empty, result.Id);
            }
            finally
            {
                _storage.Companies.Remove(result.Id);
            }
        }

        private Storage BuildStorage()
        {
            var storage = new Storage();

            var companyGuid = Guid.NewGuid();
            storage.Companies.Add(companyGuid, new Company()
            {
                Id = companyGuid,
                Name = "Test company",
                VatNumber = "BE0123456789",
                EstablishmentAddress = new Address()
                {
                    AddressLine1 = "Company Address Line 1",
                    AddressLine2 = null,
                    PostCode = "1234",
                    City = "Company City"
                },
                SubsidiaryAddresses = new List<Address>()

            });


            return storage;
        }
    }
}
