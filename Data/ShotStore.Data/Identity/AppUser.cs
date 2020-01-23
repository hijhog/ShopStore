using Microsoft.AspNetCore.Identity;
using ShotStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShotStore.Data.Identity
{
    public class AppUser : IdentityUser
    {
        public ICollection<Order> Orders { get; set; }
    }
}
