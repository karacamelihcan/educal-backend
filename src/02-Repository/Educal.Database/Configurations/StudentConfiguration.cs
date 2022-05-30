using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Educal.Database.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(std => std.Id);
            builder.Property(std => std.Id).UseIdentityColumn();
            builder.Property(std => std.Name).IsRequired();
            builder.Property(std => std.Surname).IsRequired();
            builder.Property(std => std.Email).IsRequired();
            builder.HasIndex(std => std.Email).IsUnique();
            builder.HasOne(std => std.Classroom).WithMany(cls => cls.Students);
        }
    }
}