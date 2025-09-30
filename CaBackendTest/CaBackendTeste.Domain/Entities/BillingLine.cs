namespace CaBackendTest.Domain.Entities
{
    public class BillingLine
    {
        public BillingLine(Guid productId, string description, int quantity, decimal unitPrice, decimal subTotal)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
            SubTotal = subTotal;
        }
        public BillingLine()
        {
            
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid BillingId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
        public Product Product { get; set; }
        public Billing Billing { get; set; }
    }
}
