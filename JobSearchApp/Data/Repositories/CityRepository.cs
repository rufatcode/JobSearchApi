using System;
using System.ComponentModel;
using Data.DAL;
using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
	public class CityRepository:Repository<City>, ICityRepository
    {
		public CityRepository(DataContext context) : base(context)
        {
		}
	}
}

