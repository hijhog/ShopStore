﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStore.Data.Entities
{
    public class Category : BaseEntity
    {
        public ICollection<Product> Products { get; set; }
    }
}