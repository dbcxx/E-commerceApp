﻿using Microsoft.AspNetCore.Identity;

namespace E_commerceApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
