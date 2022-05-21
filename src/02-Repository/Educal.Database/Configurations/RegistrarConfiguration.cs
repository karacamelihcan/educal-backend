using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Educal.Database.Configurations
{
    public class RegistrarConfiguration : IEntityTypeConfiguration<Registrar>
    {
        public void Configure(EntityTypeBuilder<Registrar> builder)
        {
            builder.HasKey(rgs => rgs.Id);
            builder.Property(rgs => rgs.Id).UseIdentityColumn();
            builder.Property(rgs => rgs.Name).IsRequired();
            builder.Property(rgs => rgs.Surname).IsRequired();
            builder.Property(rgs => rgs.Email).IsRequired();
            builder.HasIndex(rgs => rgs.Email).IsUnique();
        }
    }
}