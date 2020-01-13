using Genesis.Core.Models;
using Genesis.Data.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Genesis.Data.EntityFramework.Tests
{
    public class CompanyRepositoryTests
    {
        private readonly CompanyRepository _repository;
        private readonly GenesisContext _context;

        public CompanyRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<GenesisContext>()
                .UseInMemoryDatabase("TestInMemoryDb")
                .Options;

            _context = new GenesisContext(options);
            BuildContext();
            _repository = new CompanyRepository(_context);
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
                var companyEntityToDelete = await _context.Companies.FirstOrDefaultAsync(c => c.Id == result.Id);
                if (companyEntityToDelete != null)
                {
                    _context.Companies.Remove(companyEntityToDelete);
                    await _context.SaveChangesAsync();
                }
            }
        }

        private void RemoveCompanyById(Guid id)
        {
        }

        private void BuildContext()
        {
            var companyGuid = Guid.NewGuid();
            var companyEntity = CompanyEntity.MapFromBusinessObject(new Company()
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
            _context.Companies.Add(companyEntity);
            _context.SaveChanges();
        }


    }
}
