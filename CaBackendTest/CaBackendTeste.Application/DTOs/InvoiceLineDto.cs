using System.Text.Json.Serialization;

namespace CaBackendTest.Application.DTOs
{
    public class InvoiceLineDto
    {
        [JsonPropertyName("productId")]
        public Guid ProductId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("unit_price")]
        public decimal UnitPrice { get; set; }

        [JsonPropertyName("subtotal")]
        public decimal Subtotal { get; set; }
    }
}
