using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Educal.Database.Configurations
{
    public class WorkingTimeConfigurations : IEntityTypeConfiguration<WorkingTime>
    {
        public void Configure(EntityTypeBuilder<WorkingTime> builder)
        {
            builder.HasKey(time => time.Id);
            builder.Property(time => time.Id).UseIdentityColumn();
            builder.Property(time => time.Day).IsRequired();
            builder.Property(time => time.StartTime).IsRequired();
            builder.Property(time => time.EndTime).IsRequired();

            builder.HasOne(time => time.Instructor).WithMany(inst => inst.WorkingTimes);
        }
    }
}