using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educal.Database.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EducalDbContext _context;

        public UnitOfWork(EducalDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}