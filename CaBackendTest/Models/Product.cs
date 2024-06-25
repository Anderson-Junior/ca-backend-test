using System.ComponentModel.DataAnnotations;

namespace CaBackendTest.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
