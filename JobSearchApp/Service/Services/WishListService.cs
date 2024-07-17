using System;
using System.Linq.Expressions;
using Domain.Entities;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class WishListService: IWishListService
    {
		public WishListService()
		{
		}

        public Task<ResponseObj> Create(WishList entity)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObj> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObj> DeleteFromDB(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<WishList>> GetAll(Expression<Func<WishList, bool>> predicate = null, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<WishList> GetEntity(Expression<Func<WishList, bool>> predicate = null, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(Expression<Func<WishList, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObj> Update(WishList entity)
        {
            throw new NotImplementedException();
        }
    }
}

