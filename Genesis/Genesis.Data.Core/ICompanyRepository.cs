
using Genesis.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Genesis.Data.Core
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Task<IEnumerable<Contact>> GetContactsAsync(Guid id);

        Task<Contact> AddContactAsync(Guid id, Guid contactId);

        Task RemoveContactAsync(Guid id, Guid contactId);
    }
}
