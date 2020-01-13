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
    public class ContactRepository : IContactRepository
    {
        private readonly GenesisContext _context;
        public ContactRepository(GenesisContext context)
        {
            _context = context;
        }

        public async Task<Contact> CreateAsync(Contact obj)
        {
            var contactEntity = ContactEntity.MapFromBusinessObject(obj);
            contactEntity.Id = Guid.NewGuid();
            await _context.Contacts.AddAsync(contactEntity);
            await _context.SaveChangesAsync();
            return contactEntity.MapToBusinessObject();
        }

        public async Task DeleteAsync(Guid id)
        {
            var contactEntity = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);

            if (contactEntity != null)
            {
                _context.Contacts.Remove(contactEntity);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public Task<IEnumerable<Contact>> GetAllAsync()
        {
            return Task.Run(() =>
            {
                return (IEnumerable<Contact>)_context.Contacts.Select(c => c.MapToBusinessObject());
            });
        }

        public async Task<Contact> GetAsync(Guid id)
        {
            var contactEntity = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);

            if (contactEntity != null)
            {
                return contactEntity.MapToBusinessObject();
            }

            throw new ArgumentOutOfRangeException();
        }

        public async Task<Contact> UpdateAsync(Guid id, Contact contact)
        {
            var contactEntity = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);

            if (contactEntity != null)
            {
                contactEntity.Name = contact.Name;
                contactEntity.AddressLine1 = contact.Address?.AddressLine1;
                contactEntity.AddressLine2 = contact.Address?.AddressLine2;
                contactEntity.PostCode = contact.Address?.PostCode;
                contactEntity.City = contact.Address?.City;
                contactEntity.Type = contact.Type;
                contactEntity.VatNumber = contact.VatNumber;
                await _context.SaveChangesAsync();

                return contactEntity.MapToBusinessObject();

            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
