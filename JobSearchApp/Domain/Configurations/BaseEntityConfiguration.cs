using System;
using Domain.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public BaseEntityConfiguration()
        {
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(c => c.RegUser)
              .IsRequired()
              .HasDefaultValue("System");
            builder.Property(c => c.CreatedAt)
                .HasDefaultValue(DateTime.UtcNow.AddHours(8));
        }
    }
}

