using System.Text.Json.Serialization;

namespace CaBackendTest.Domain.Entities
{
    public class Customer
    {
        public Customer(string name, string email, string address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Address = address;
        }

        public Customer()
        {
            
        }
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        [JsonIgnore]
        public IEnumerable<Billing> Billings { get; set; }
    }
}
