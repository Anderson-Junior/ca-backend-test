using System.ComponentModel.DataAnnotations;

namespace CaBackendTest.Domain.DTOs
{
    public class UpsertCustomerDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100, ErrorMessage = "Email must not exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(150, ErrorMessage = "Address must not exceed 150 characters.")]
        public string Address { get; set; }
    }
}
