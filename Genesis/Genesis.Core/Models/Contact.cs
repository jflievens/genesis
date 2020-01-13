using Genesis.Core.Enums;
using Genesis.Core.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Core.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Required]
        public Address Address { get; set; }
        public ContactType Type { get; set; }
        [RequiredIfFreelance]
        public string VatNumber { get; set; }
    }
}
