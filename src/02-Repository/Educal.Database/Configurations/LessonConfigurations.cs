using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Educal.Database.Configurations
{
    public class LessonConfigurations : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(les => les.Id);
            builder.Property(les => les.Id).UseIdentityColumn();
            builder.Property(les => les.Guid).IsRequired();
            builder.Property(les => les.Name).IsRequired();
            builder.HasIndex(les => les.Name).IsUnique();
        }
    }
}