using System;
using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Dtos.WishListDtos
{
	public class PostWishListDto
	{
        public string UserId { get; set; }
        public int JobId { get; set; }
        public PostWishListDto()
		{
		}
	}
}

