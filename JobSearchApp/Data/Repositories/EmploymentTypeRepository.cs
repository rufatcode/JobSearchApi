using System;
using Data.DAL;
using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
	public class EmploymentTypeRepository:Repository<EmploymentType>, IEmploymentTypeRepository
    {
		public EmploymentTypeRepository(DataContext context) : base(context)
        {
		}
	}
}

