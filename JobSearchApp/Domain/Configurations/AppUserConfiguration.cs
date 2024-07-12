using System;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public AppUserConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<AppUser> builder)
        {

            string AdminId = "56e9e4e5-22a8-45a7-ab6c-999180f9d2e2";
            string SupperAdminId = "81c5f0b8-be89-4e4a-88ba-01ca7f6244dd";
            builder.Property(a => a.AddedBy).HasDefaultValue("System");
            builder.Property(a => a.CreatedAt)
               .HasDefaultValue(DateTime.UtcNow.AddHours(4));

            AppUser Admin = new AppUser
            {
                Id = AdminId,
                Email = "rufatri@code.edu.az",
                NormalizedEmail = "RUFATRI@CODE.EDU.AZ",
                NormalizedUserName = "RUFATCODE",
                UserName = "RufatCode",
                FullName = "Rufat Ismayilov",
                EmailConfirmed = true,
                IsActive = true,
                PhoneNumber = "+994513004484",
                PhoneNumberConfirmed = true,
                CreatedAt = DateTime.Now,
                AddedBy = "System",

            };
            AppUser SupperAdmin = new AppUser
            {
                Id = SupperAdminId,
                Email = "rufetismayiliv@gmail.com",
                NormalizedEmail = "RUFETISMAYILIV@GMAIL.COM",
                NormalizedUserName = "RUFAT_2003",
                UserName = "Rufat_2003",
                FullName = "Rufat Ismayilov",
                EmailConfirmed = true,
                IsActive = true,
                PhoneNumber = "+994513004484",
                PhoneNumberConfirmed = true,
                CreatedAt = DateTime.Now,
                AddedBy = "System",
                LockoutEnabled = false,

            };
            PasswordHasher<AppUser> hasher = new PasswordHasher<AppUser>();
            string AdminPassword = hasher.HashPassword(Admin, "Rufat123.");
            string SupperAdminPassword = hasher.HashPassword(SupperAdmin, "Rufat123.");
            Admin.PasswordHash = AdminPassword;
            SupperAdmin.PasswordHash = SupperAdminPassword;
            builder.HasData(Admin);
            builder.HasData(SupperAdmin);
        }
    }
}

