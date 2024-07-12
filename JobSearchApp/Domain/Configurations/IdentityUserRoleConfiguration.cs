using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public IdentityUserRoleConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            string AdminRoleId = "20cc338d-4993-4a04-87b5-0f8fc208ceba";
            string SupperAdminRoleId = "fee579ed-a458-41ad-b9f4-4299f5a50029";
            string AdminId = "56e9e4e5-22a8-45a7-ab6c-999180f9d2e2";
            string SupperAdminId = "81c5f0b8-be89-4e4a-88ba-01ca7f6244dd";
            builder.HasData(new IdentityUserRole<string>
            {
                UserId = AdminId,
                RoleId = AdminRoleId

            },
            new IdentityUserRole<string>
            {
                UserId = SupperAdminId,
                RoleId = SupperAdminRoleId,


            });
        }
    }
}

