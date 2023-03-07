using System.ComponentModel.DataAnnotations;

namespace Mc_Task.DTO
{
    public class ProductViewModel
    {

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int ProductPrice { get; set; }

        public int ProductQuantity { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
    }
}
