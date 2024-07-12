using System;
using Data.DAL;
using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
	public class AdvertaismetRepository:Repository<Advertaismet>, IAdvertaismetRepository
    {
		public AdvertaismetRepository(DataContext context) : base(context)
        {
		}
	}
}

