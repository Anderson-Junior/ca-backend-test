using System.Text.Json.Serialization;

namespace CaBackendTest.Application.DTOs
{
    public class InvoiceDto
    {
        [JsonPropertyName("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonPropertyName("customer")]
        public CustomerDto Customer { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("due_date")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("lines")]
        public List<InvoiceLineDto> Lines { get; set; }
    }
}
