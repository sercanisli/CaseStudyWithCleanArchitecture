﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category:Entity<Guid>
    {
        public string Name { get; set; }
        public int StockLimit { get; set; }
        public virtual ICollection<Product> Products { get; set; }


        public Category()
        {
            Products=new HashSet<Product>();
        }
        public Category(Guid id, string name):this()
        {
            Id = id;
            Name = name;
        }
    }
}
