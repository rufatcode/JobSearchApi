using System;
using Data.DAL;
using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
	public class PhoneNumberRepository: Repository<PhoneNumber>, IPhoneNumberRespository
    {
		public PhoneNumberRepository(DataContext context) : base(context)
        {
		}
	}
}

