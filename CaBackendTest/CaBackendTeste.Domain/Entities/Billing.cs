namespace CaBackendTest.Domain.Entities
{
    public class Billing
    {
        public Billing(string invoiceNumber, DateTime date, DateTime dueDate, string currency, decimal totalAmount, Guid customerId, Customer customer, ICollection<BillingLine> billingLines)
        {
            Id = Guid.NewGuid();
            InvoiceNumber = invoiceNumber;
            Date = date;
            DueDate = dueDate;
            Currency = currency;
            TotalAmount = totalAmount;
            CustomerId = customerId;
            Customer = customer;
            BillingLines = billingLines;
        }
        public Billing()
        {
            
        }

        public Guid Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public string Currency { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<BillingLine> BillingLines { get; set; }
    }
}
