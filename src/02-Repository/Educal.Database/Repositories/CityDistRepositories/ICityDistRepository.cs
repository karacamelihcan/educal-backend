using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;

namespace Educal.Database.Repositories.CityDistRepositories
{
    public interface ICityDistRepository
    {
        Task<IEnumerable<City>> GetCities();
        Task<IEnumerable<District>> GetDistricts(int CityId);
    }
}