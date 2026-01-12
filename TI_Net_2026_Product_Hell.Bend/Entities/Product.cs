namespace TI_Net_2026_Product_Hell.Bend.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Image { get; set; } = null!;
        public int Price { get; set; }
    }
}
