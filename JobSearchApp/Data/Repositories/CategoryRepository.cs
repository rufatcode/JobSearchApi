using System;
using Data.DAL;
using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
	public class CategoryRepository:Repository<Category>, ICategoryRepository
    {
		public CategoryRepository(DataContext context) : base(context)
        {
		}
	}
}

