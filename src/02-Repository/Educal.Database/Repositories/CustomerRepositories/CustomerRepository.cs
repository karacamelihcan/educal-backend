using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Core.Models;
using Educal.Database.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Educal.Database.Repositories.CustomerRepositories
{
    public class CustomerRepository : BaseRepository<Customer>,ICustomerRepository
    {
        private readonly EducalDbContext _context;

        public CustomerRepository(EducalDbContext context)
        {
            _context = context;
        }

        public override async Task AddAsync(Customer entity)
        {
            await _context.Customers.AddAsync(entity);
        }

        public override async Task AddRangeAsync(IEnumerable<Customer> entities)
        {
            await _context.Customers.AddRangeAsync(entities);
        }

        public override void Delete(Customer entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public async override Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.Where(x => x.IsDeleted == false).ToListAsync();;
        }

        public async Task<Customer> GetByEmail(string email)
        {
            return await _context.Customers.Where(x => x.Email == email && x.IsDeleted == false).FirstOrDefaultAsync();
        }

        public override async Task<Customer> GetByGuidAsync(Guid Id)
        {
            return await _context.Customers.Where(c => c.Guid == Id && c.IsDeleted == false)
                                           .FirstOrDefaultAsync();
        }

        public async override Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.Where(c => c.Id == id && c.IsDeleted == false)
                                           .FirstOrDefaultAsync();
        }

        public override void Update(Customer entity)
        {
            _context.Customers.Update(entity);
        }

        public override async Task<IQueryable<Customer>> Where(Expression<Func<Customer, bool>> expression)
        {
            return _context.Customers.Where(expression);
        }
    }
}