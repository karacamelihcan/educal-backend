using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Educal.Database.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(cstr => cstr.Id);
            builder.Property(cstr => cstr.Id).UseIdentityColumn();
            builder.Property(cstr => cstr.Name).IsRequired();
            builder.Property(cstr => cstr.Surname).IsRequired();
            builder.Property(cstr => cstr.Email).IsRequired();
            builder.HasIndex(cstr => cstr.Email).IsUnique();
        }   
    }
}