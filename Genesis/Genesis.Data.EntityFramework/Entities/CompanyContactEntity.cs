using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Genesis.Data.EntityFramework.Entities
{
    [Table("CompanyContacts")]
    public class CompanyContactEntity
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid ContactId { get; set; }
    }
}