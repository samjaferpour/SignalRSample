namespace Backend.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
