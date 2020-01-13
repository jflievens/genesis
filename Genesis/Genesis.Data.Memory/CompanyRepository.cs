using Genesis.Core.Models;
using Genesis.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Data.Memory
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly Storage _storage;
        public CompanyRepository(Storage storage)
        {
            _storage = storage;
        }

        public Task<Contact> AddContactAsync(Guid id, Guid contactId)
        {
            return Task.Run(() =>
            {
                if (_storage.Companies.ContainsKey(id) && _storage.Contacts.TryGetValue(contactId, out Contact existingContact))
                {
                    if (!_storage.CompanyContacts.TryGetValue(id, out List<Guid> contacts))
                    {
                        contacts = new List<Guid>();
                        _storage.CompanyContacts.Add(id, contacts);
                    }
                    if (!contacts.Contains(contactId))
                    {
                        contacts.Add(contactId);
                    }

                    return existingContact;
                };

                throw new ArgumentOutOfRangeException();
            });
        }

        public Task<Company> CreateAsync(Company company)
        {
            return Task.Run(() =>
            {
                company.Id = Guid.NewGuid();
                _storage.Companies.Add(company.Id, company);
                return company;
            });
        }

        public Task DeleteAsync(Guid id)
        {
            return Task.Run(() =>
            {

                if (_storage.Companies.ContainsKey(id))
                {
                    _storage.Companies.Remove(id);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }

            });
        }

        public Task<IEnumerable<Company>> GetAllAsync()
        {
            return Task.Run(() =>
            {
                return (IEnumerable<Company>)_storage.Companies.Values.ToList();
            });
        }

        public Task<Company> GetAsync(Guid id)
        {
            return Task.Run(() =>
            {
                if (_storage.Companies.TryGetValue(id, out Company existingCompany))
                {
                    return existingCompany;
                };

                throw new ArgumentOutOfRangeException();
            });
        }

        public Task<IEnumerable<Contact>> GetContactsAsync(Guid id)
        {
            return Task.Run(() =>
            {
                if (!_storage.Companies.ContainsKey(id))
                {
                    throw new ArgumentOutOfRangeException();
                }

                var result = new List<Contact>();
                if (_storage.CompanyContacts.TryGetValue(id, out List<Guid> contacts))
                {
                    foreach (var guid in contacts)
                    {
                        if (_storage.Contacts.TryGetValue(guid, out Contact contact))
                        {
                            result.Add(contact);
                        };
                    }
                }

                return (IEnumerable<Contact>)result;

            });
        }

        public Task RemoveContactAsync(Guid id, Guid contactId)
        {
            return Task.Run(() =>
            {
                if (!_storage.Companies.ContainsKey(id))
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (_storage.CompanyContacts.TryGetValue(id, out List<Guid> contacts))
                {
                    contacts.RemoveAll(guid => guid == contactId);
                }
            });
        }

        public Task<Company> UpdateAsync(Guid id, Company company)
        {
            return Task.Run(() =>
            {
                if (_storage.Companies.TryGetValue(id, out Company existingCompany))
                {
                    existingCompany.Name = company.Name;
                    existingCompany.VatNumber = company.VatNumber;
                    existingCompany.EstablishmentAddress = company.EstablishmentAddress;
                    existingCompany.SubsidiaryAddresses = company.SubsidiaryAddresses;

                    return existingCompany;
                };

                throw new ArgumentOutOfRangeException();
            });

        }
    }
}
