using System;
using AutoMapper;
using Domain.Entities;
using Service.Dtos.WishListDtos;

namespace Service.Profiles
{
	public class WishListProfile:Profile
	{
		public WishListProfile()
		{
			CreateMap<WishList, GetWishListDto>();
			CreateMap<WishList, PostWishListDto>();
			CreateMap<WishList, PutWishListDto>();
        }
	}
}

