using Genesis.Core.Models;
using System;
using System.Collections.Generic;

namespace Genesis.Data.Memory
{
    public class Storage
    {
        public Dictionary<Guid, Contact> Contacts = new Dictionary<Guid, Contact>();
        public Dictionary<Guid, Company> Companies = new Dictionary<Guid, Company>();
        public Dictionary<Guid, List<Guid>> CompanyContacts = new Dictionary<Guid, List<Guid>>();
    }
}
