namespace CaBackendTest.Domain.Entities
{
    public class Product
    {
        public Product(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Product()
        {
            
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
