using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Commons;

namespace Domain.Entities
{
	public class WishList:BaseEntity
	{
		[ForeignKey(nameof(AppUser))]
		public string UserId { get; set; }
		public AppUser AppUser { get; set; }
		[ForeignKey(nameof(Job))]
		public int JobId { get; set; }
		public Job Job { get; set; }
		public WishList()
		{
		}
	}
}

