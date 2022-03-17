using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceApp.Models
{
    public class Products
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Display(Name ="Product Color")]
        public string ProductColor { get; set; }
        [Required]
        [Display(Name ="Available")]
        public bool IsAvailable { get; set; }
        [Display(Name="Product Type")]
        [Required]
        public int ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductTypes ProductTypes { get; set; }
        [Display(Name = "Product Tag")]
        [Required]
        public int ProductTagId { get; set; }
        [ForeignKey("ProductTagId")]
        public ProductTags ProductTags { get; set; }
    }
}
