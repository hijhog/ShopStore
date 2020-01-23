﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShotStore.Data.Entities
{
    public class Store : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ProductCount { get; set; }
    }
}
