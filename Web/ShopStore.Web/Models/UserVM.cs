using ShopStore.Data.Models.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopStore.Web.Models
{
    public class UserVM
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string RoleId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
