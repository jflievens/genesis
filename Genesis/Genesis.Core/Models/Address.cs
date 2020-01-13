using System.ComponentModel.DataAnnotations;

namespace Genesis.Core.Models
{
    public class Address
    {
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string City { get; set; }
    }
}
