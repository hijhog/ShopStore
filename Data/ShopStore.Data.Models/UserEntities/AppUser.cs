using Microsoft.AspNetCore.Identity;
using ShopStore.Data.Models.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Models.UserEntities
{
    public class AppUser : IdentityUser<int>
    {
        public ICollection<Order> Orders { get; set; }
    }
}
