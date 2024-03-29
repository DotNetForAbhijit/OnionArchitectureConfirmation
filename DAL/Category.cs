﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace DAL
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
