using System.ComponentModel.DataAnnotations;

namespace ApiCustomers.Models
{
    public class Customer
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Cep Inválido.")]
        public string Cep { get; set; }

        public string Street { get; set; }

        public string City { get; set; }
        
        public string State { get; set; }
    }
}
