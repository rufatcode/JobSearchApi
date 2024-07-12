using System;
using Data.DAL;
using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
	public class WishListRepository:Repository<WishList>, IWishListRepository
    {
		public WishListRepository(DataContext context) : base(context)
        {
		}
	}
}

