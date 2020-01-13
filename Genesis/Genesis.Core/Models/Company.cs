using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Core.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string VatNumber { get; set; }
        [Required]
        public Address EstablishmentAddress { get; set; }
        public ICollection<Address> SubsidiaryAddresses { get; set; }
    }
}
