using Microsoft.AspNetCore.Identity;
using ShopStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public ICollection<Order> Orders { get; set; }
    }
}
