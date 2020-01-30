using ShopStore.Data.Contract.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Services.Contract.Models
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public string Role { get; set; }
        public Gender Gender { get; set; }
        public string Password { get; set; }
    }
}
