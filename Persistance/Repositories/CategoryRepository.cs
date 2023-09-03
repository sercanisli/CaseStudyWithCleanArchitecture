﻿using Application.Services.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CategoryRepository : EntityFrameworkRepositoryBase<Category, Guid, BaseDbContext>, ICategoryRepository
    {
        public CategoryRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
