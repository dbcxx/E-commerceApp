using System.ComponentModel.DataAnnotations;

namespace E_commerceApp.Models
{
    public class ProductTags
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Tag")]
        public string ProductTag { get; set; }
    }
}
