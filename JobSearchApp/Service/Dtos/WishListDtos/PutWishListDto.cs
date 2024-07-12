using System;
using Service.Dtos.BaseDtos;

namespace Service.Dtos.WishListDtos
{
	public class PutWishListDto:PutBaseDto
	{
        public string UserId { get; set; }
        public int JobId { get; set; }
        public PutWishListDto()
		{
		}
	}
}

