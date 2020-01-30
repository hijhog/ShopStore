using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Contract.BusinessEntities
{
    public enum OrderStatus
    {
        Accepted = 1,
        Completed = 2,
        Rejected = 3
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public enum RoleEnum
    {
        Admin = 1,
        Operator = 2,
        User = 3
    }
}
