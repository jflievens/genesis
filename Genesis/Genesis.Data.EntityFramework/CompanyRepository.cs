using Genesis.Core.Models;
using Genesis.Data.Core;
using Genesis.Data.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Data.EntityFramework
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly GenesisContext _context;
        public CompanyRepository(GenesisContext context)
        {
            _context = context;
        }

        public async Task<Company> CreateAsync(Company obj)
        {
            var companyEntity = CompanyEntity.MapFromBusinessObject(obj);
            companyEntity.Id = Guid.NewGuid();
            await _context.Companies.AddAsync(companyEntity);
            await _context.SaveChangesAsync();
            return companyEntity.MapToBusinessObject();
        }

        public async Task DeleteAsync(Guid id)
        {
            var companyEntity = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
            if (companyEntity != null)
            {
                _context.Companies.Remove(companyEntity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public Task<IEnumerable<Company>> GetAllAsync()
        {
            return Task.Run(() =>
            {
                return (IEnumerable<Company>)_context.Companies.Include(c => c.Addresses).Select(c => c.MapToBusinessObject());
            });
        }

        public async Task<Company> GetAsync(Guid id)
        {
            var companyEntity = await _context.Companies.Include(c => c.Addresses).FirstOrDefaultAsync(c => c.Id == id);
            if (companyEntity != null)
            {
                return companyEntity.MapToBusinessObject();
            }

            throw new ArgumentOutOfRangeException();
        }

        public async Task<Company> UpdateAsync(Guid id, Company company)
        {
            var companyEntity = await _context.Companies.Include(c => c.Addresses).FirstOrDefaultAsync(c => c.Id == id);

            if (companyEntity != null)
            {
                companyEntity.Name = company.Name;
                companyEntity.VatNumber = company.VatNumber;
                companyEntity.Addresses = new List<CompanyAddressEntity>();
                if (company.EstablishmentAddress != null)
                {
                    var establishmentAddressEntity = CompanyAddressEntity.MapFromBusinessObject(company.EstablishmentAddress, true);
                    companyEntity.Addresses.Add(establishmentAddressEntity);
                }

                if (company.SubsidiaryAddresses != null)
                {
                    foreach (var subsidiaryAddress in company.SubsidiaryAddresses)
                    {
                        var subsidiaryAddressEntity = CompanyAddressEntity.MapFromBusinessObject(subsidiaryAddress, false);
                        companyEntity.Addresses.Add(subsidiaryAddressEntity);
                    }
                }

                await _context.SaveChangesAsync();

                return companyEntity.MapToBusinessObject();
            }

            throw new ArgumentOutOfRangeException();
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync(Guid id)
        {
            var result = new List<Contact>();
            var contacts = from cc in _context.CompanyContacts
                           join c in _context.Contacts on cc.ContactId equals c.Id
                           where cc.CompanyId == id
                           select c.MapToBusinessObject();

            return await contacts.ToListAsync();
        }

        public async Task<Contact> AddContactAsync(Guid id, Guid contactId)
        {
            var contactEntity = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);
            var companyEntity = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);

            if (contactEntity == null || companyEntity == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            var existingCompanyContact = await _context.CompanyContacts.FirstOrDefaultAsync(cc => cc.CompanyId == id && cc.ContactId == contactId);
            if (existingCompanyContact == null)
            {
                var newCompanyContact = new CompanyContactEntity() { CompanyId = id, ContactId = contactId };
                await _context.CompanyContacts.AddAsync(newCompanyContact);
                await _context.SaveChangesAsync();
            }

            return contactEntity.MapToBusinessObject();
        }

        public async Task RemoveContactAsync(Guid id, Guid contactId)
        {
            var existingCompanyContact = await _context.CompanyContacts.FirstOrDefaultAsync(cc => cc.CompanyId == id && cc.ContactId == contactId);

            if (existingCompanyContact != null)
            {
                _context.CompanyContacts.Remove(existingCompanyContact);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
