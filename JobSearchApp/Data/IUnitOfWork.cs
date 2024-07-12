using System;
using Data.Repositories.Interfaces;

namespace Data
{
	public interface IUnitOfWork
	{
		 IAdvertaismetRepository AdvertaismetRepository { get; }
		 ICategoryRepository CategoryRepository { get; }
		 ICityRepository CityRepository { get; }
		 ICompanyRepository CompanyRepository { get; }
		 ICompanyContactRepository CompanyContactRepository { get; }
		 IEmploymentTypeRepository EmploymentTypeRepository { get; }
		 IJobInformationRepository JobInformationRepository { get; }
		 IJobInformationTypeRepository JobInformationTypeRepository { get; }
		 IJobRepository JobRepository { get; }
		 IPhoneNumberHeadlingRepository PhoneNumberHeadlingRepository { get; }
		 IPositionRepository PositionRepository { get; }
		 IWishListRepository WishListRepository { get; }
    }
}

