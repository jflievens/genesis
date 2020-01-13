using Genesis.Core.Models;
using Genesis.Data.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Genesis.Data.Memory
{
    public class ContactRepository : IContactRepository
    {

        private readonly Storage _storage;
        public ContactRepository(Storage storage)
        {
            _storage = storage;
        }

        public Task<Contact> CreateAsync(Contact contact)
        {
            return Task.Run(() =>
            {
                contact.Id = Guid.NewGuid();
                _storage.Contacts.Add(contact.Id, contact);
                return contact;
            });
        }

        public Task DeleteAsync(Guid id)
        {
            return Task.Run(() =>
            {
                if (_storage.Contacts.ContainsKey(id))
                {
                    _storage.Contacts.Remove(id);
                    foreach (var kvp in _storage.CompanyContacts)
                    {
                        if (kvp.Value != null)
                        {
                            kvp.Value.RemoveAll(guid => guid == id);
                        }
                    }
                }

                throw new ArgumentOutOfRangeException();
            });
        }

        public Task<IEnumerable<Contact>> GetAllAsync()
        {
            return Task.Run(() =>
            {
                return (IEnumerable<Contact>)_storage.Contacts.Values;
            });
        }

        public Task<Contact> GetAsync(Guid id)
        {
            return Task.Run(() =>
            {
                if (_storage.Contacts.TryGetValue(id, out Contact existingContact))
                {
                    return existingContact;
                }

                throw new ArgumentOutOfRangeException();
            });
        }

        public Task<Contact> UpdateAsync(Guid id, Contact contact)
        {
            return Task.Run(() =>
            {
                if (_storage.Contacts.TryGetValue(id, out Contact existingContact))
                {
                    existingContact.Name = contact.Name;
                    existingContact.Type = contact.Type;
                    existingContact.VatNumber = contact.VatNumber;
                    existingContact.Address = contact.Address;

                    return existingContact;
                }

                throw new ArgumentOutOfRangeException();
            });
        }
    }
}
