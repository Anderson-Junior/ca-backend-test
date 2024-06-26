namespace CaBackendTest.Domain.Entities
{
    public sealed class Customer : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
