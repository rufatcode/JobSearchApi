using System;
using Data.DAL;
using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
	public class JobRepository:Repository<Job>, IJobRepository
    {
		public JobRepository(DataContext context) : base(context)
        {
		}
	}
}

