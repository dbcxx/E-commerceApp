using System.ComponentModel.DataAnnotations;

namespace E_commerceApp.Areas.Admin.Models
{
    public class RoleUser
    {
        [Required]
        [Display(Name = "User")]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string RoleId { get; set; }
    }
}

