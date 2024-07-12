using System;
using Data.DAL;
using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
	public class JobInformationRepository:Repository<JobInformation>, IJobInformationRepository
    {
		public JobInformationRepository(DataContext context) : base(context)
        {
		}
	}
}

