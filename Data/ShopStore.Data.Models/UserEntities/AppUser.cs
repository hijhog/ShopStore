using Microsoft.AspNetCore.Identity;
using ShopStore.Data.Contract.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Models.UserEntities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
