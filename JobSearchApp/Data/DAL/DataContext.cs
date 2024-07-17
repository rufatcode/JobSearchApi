using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using Domain.Entities;
using Domain.Configurations;
using System.Reflection.Emit;

namespace Data.DAL
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DbSet<Advertaismet> Advertaismets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyContact> CompanyContacts { get; set; }
        public DbSet<EmploymentType> EmploymentTypes { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobInformation> JobInformations { get; set; }
        public DbSet<JobInformationType> JobInformationTypes { get; set; }
        public DbSet<PhoneNumberHeadling> PhoneNumberHeadlings { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public PhoneNumber  PhoneNumber { get; set; }
        public DataContext(DbContextOptions dbContext) : base(dbContext)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);
            base.OnModelCreating(builder);
        }
       
    }
}

