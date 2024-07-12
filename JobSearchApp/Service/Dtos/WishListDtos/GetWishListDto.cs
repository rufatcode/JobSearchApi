using System;
using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Service.Dtos.BaseDtos;
using Service.Dtos.JobDtos;

namespace Service.Dtos.WishListDtos
{
	public class GetWishListDto:GetBaseDto
	{
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public int JobId { get; set; }
        public GetJobDto Job { get; set; }
        public GetWishListDto()
		{
		}
	}
}

