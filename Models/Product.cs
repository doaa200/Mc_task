namespace Mc_Task.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

    }
}
