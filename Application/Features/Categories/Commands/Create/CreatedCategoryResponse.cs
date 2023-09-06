﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands.Create
{
    public class CreatedCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StockLimit { get; set; }
    }
}