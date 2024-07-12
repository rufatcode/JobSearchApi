using System;
using System.Windows.Input;
using Data.DAL;
using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
	public class CompanyRepository:Repository<Company>, ICompanyRepository
    {
		public CompanyRepository(DataContext context) : base(context)
        {
		}
	}
}

