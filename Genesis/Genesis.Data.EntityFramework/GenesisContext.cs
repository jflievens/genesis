using Genesis.Data.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Genesis.Data.EntityFramework
{
    public class GenesisContext : DbContext
    {
        public GenesisContext(DbContextOptions<GenesisContext> options) : base(options) { }

        public DbSet<CompanyEntity> Companies { get; set; }

        public DbSet<CompanyAddressEntity> CompanyAddresse { get; set; }

        public DbSet<ContactEntity> Contacts { get; set; }

        public DbSet<CompanyContactEntity> CompanyContacts { get; set; }
    }
}
