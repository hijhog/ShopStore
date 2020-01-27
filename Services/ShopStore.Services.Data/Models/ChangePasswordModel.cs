using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Services.Data.Models
{
    public class ChangePasswordModel
    {
        public Guid UserId { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
