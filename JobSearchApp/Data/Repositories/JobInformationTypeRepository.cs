using System;
using Data.DAL;
using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
	public class JobInformationTypeRepository:Repository<JobInformationType>, IJobInformationTypeRepository
    {
		public JobInformationTypeRepository(DataContext context) : base(context)
        {
		}
	}
}

