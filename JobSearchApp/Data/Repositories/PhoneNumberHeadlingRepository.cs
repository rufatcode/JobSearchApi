using System;
using Data.DAL;
using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories
{
	public class PhoneNumberHeadlingRepository:Repository<PhoneNumberHeadling>, IPhoneNumberHeadlingRepository
    {
		public PhoneNumberHeadlingRepository(DataContext context) : base(context)
        {
		}
	}
}

