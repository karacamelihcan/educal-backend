using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Educal.Database.Configurations
{
    public class DistrictConfigurations : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(disc => disc.Id);
            builder.Property(disc => disc.Id).UseIdentityColumn();
            builder.Property(disc => disc.Name).IsRequired();

            builder.HasOne(disc => disc.City).WithMany(city => city.Districts);
        }
    }
}