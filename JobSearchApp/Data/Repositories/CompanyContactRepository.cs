using System;
using Data.DAL;
using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
	public class CompanyContactRepository:Repository<CompanyContact>, ICompanyContactRepository
    {
		public CompanyContactRepository(DataContext context) : base(context)
        {
		}
	}
}

