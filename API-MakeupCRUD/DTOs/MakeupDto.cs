namespace API_MakeupCRUD.DTOs
{
    public class MakeupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandID { get; set; }
        public decimal Volume { get; set; }
        public decimal Price { get; set; }
        public string? Type { get; set; }

    }
}
