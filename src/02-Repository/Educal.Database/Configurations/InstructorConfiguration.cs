using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Educal.Database.Configurations
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(inst => inst.Id);
            builder.Property(inst => inst.Id).UseIdentityColumn();
            builder.Property(inst => inst.Name).IsRequired();
            builder.Property(inst => inst.Surname).IsRequired();
            builder.Property(inst => inst.Email).IsRequired();
            builder.HasIndex(inst => inst.Email).IsUnique();

            builder.HasMany(inst => inst.WorkingTimes).WithOne(time => time.Instructor);
            builder.HasMany(inst => inst.Lessons).WithMany(less => less.Instructors);
        }
    }
}