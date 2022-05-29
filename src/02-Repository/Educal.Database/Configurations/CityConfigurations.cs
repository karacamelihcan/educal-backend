using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Educal.Database.Configurations
{
    public class CityConfigurations : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(city => city.Id);
            builder.Property(city => city.Id).UseIdentityColumn();
            builder.Property(city => city.Name).IsRequired();
            builder.HasIndex(city => city.Name).IsUnique();
        }
    }
}