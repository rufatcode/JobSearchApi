using System;
using Data.DAL;
using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
	public class PositionRepository:Repository<Position>, IPositionRepository
    {
		public PositionRepository(DataContext context) : base(context)
        {
		}
	}
}

