using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Educal.Database.Repositories.CityDistRepositories
{
    public class CityDistRepository : ICityDistRepository
    {
        private readonly EducalDbContext _context;

        public CityDistRepository(EducalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            return await _context.Cities.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<District>> GetDistricts(int CityId)
        {
            return await _context.Districts.Where(dist => dist.City.Id == CityId).AsNoTracking().ToListAsync();
        }
    }
}