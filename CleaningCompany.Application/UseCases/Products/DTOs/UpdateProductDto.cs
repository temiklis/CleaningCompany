namespace CleaningCompany.Application.UseCases.Products.DTOs
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public string Difficulty { get; set; }
    }
}
