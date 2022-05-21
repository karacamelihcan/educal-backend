using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Educal.Database.Configurations
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Manager> builder)
        {
            builder.HasKey(mng => mng.Id);
            builder.Property(mng => mng.Id).UseIdentityColumn();
            builder.Property(mng => mng.Name).IsRequired();
            builder.Property(mng => mng.Surname).IsRequired();
            builder.Property(mng => mng.Email).IsRequired();
            builder.HasIndex(mng => mng.Email).IsUnique();
        }
    }
}