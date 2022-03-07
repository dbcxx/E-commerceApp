using System.ComponentModel.DataAnnotations;

namespace E_commerceApp.Models
{
    public class ProductTypes
    {
        public int Id { get; set; }

        [Required]
        public string ProductType { get; set; }
    }
}
