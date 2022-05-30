using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Educal.Database.Configurations
{
    public class ClassroomConfigurations : IEntityTypeConfiguration<Classroom>
    {
        public void Configure(EntityTypeBuilder<Classroom> builder)
        {
            builder.HasKey(cls => cls.Id);
            builder.Property(cls => cls.Id).UseIdentityColumn();

            builder.HasOne(cls => cls.Instructor).WithMany(inst => inst.Classrooms);
            builder.HasMany(cls => cls.Students).WithOne(std => std.Classroom);
            builder.HasOne(cls => cls.Lesson).WithMany(less => less.Classrooms);
            
        }
    }
}