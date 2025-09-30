using System.Text.Json.Serialization;

namespace CaBackendTest.Application.DTOs
{
    public class CustomerDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}
