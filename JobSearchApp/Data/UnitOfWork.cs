using System;
using Data.DAL;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data
{
	public class UnitOfWork: IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
            AdvertaismetRepository = new AdvertaismetRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            CityRepository = new CityRepository(_context);
            CompanyRepository = new CompanyRepository(_context);
            CompanyContactRepository = new CompanyContactRepository(_context);
            EmploymentTypeRepository = new EmploymentTypeRepository(_context);
            JobInformationRepository = new JobInformationRepository(_context);
            JobInformationTypeRepository = new JobInformationTypeRepository(_context);
            JobRepository = new JobRepository(_context);
            PhoneNumberHeadlingRepository = new PhoneNumberHeadlingRepository(_context);
            PositionRepository = new PositionRepository(_context);
            WishListRepository = new WishListRepository(_context);
        }

        public IAdvertaismetRepository AdvertaismetRepository { get; private set; }

        public ICategoryRepository CategoryRepository { get; private set; }

        public ICityRepository CityRepository { get; private set; }

        public ICompanyRepository CompanyRepository { get; private set; }

        public ICompanyContactRepository CompanyContactRepository { get; private set; }

        public IEmploymentTypeRepository EmploymentTypeRepository { get; private set; }

        public IJobInformationRepository JobInformationRepository { get; private set; }

        public IJobInformationTypeRepository JobInformationTypeRepository { get; private set; }

        public IJobRepository JobRepository { get; private set; }

        public IPhoneNumberHeadlingRepository PhoneNumberHeadlingRepository { get; private set; }

        public IPositionRepository PositionRepository { get; private set; }

        public IWishListRepository WishListRepository { get; private set; }

        public async Task<int> Complate()
        {
            return await _context.SaveChangesAsync();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

