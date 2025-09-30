using System.ComponentModel.DataAnnotations;

namespace CaBackendTest.Domain.DTOs
{
    public class UpsertProductDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string Name { get; set; }
    }
}
