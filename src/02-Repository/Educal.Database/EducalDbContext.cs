using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Educal.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Educal.Database
{
    public class EducalDbContext : DbContext
    {
        public EducalDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Registrar> Registrars { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<UserRefreshToken> RefreshTokens { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}