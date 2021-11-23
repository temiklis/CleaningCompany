namespace CleaningCompany.Application.UseCases.Materials.DTOs
{
    public class MaterialWithProductsStringDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Products { get; set; }
    }
}
