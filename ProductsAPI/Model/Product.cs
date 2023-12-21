using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.Model
{
    public class Product
    {
        [Required]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }
        public int ProductQunatity { get; set; }
        public int ProductPrice { get; set; }
    }
}
