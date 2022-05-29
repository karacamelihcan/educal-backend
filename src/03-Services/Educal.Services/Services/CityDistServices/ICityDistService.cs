using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Contract.Responses;
using Educal.Core.Models;

namespace Educal.Services.Services.CityDistServices
{
    public interface ICityDistService
    {
        Task<ApiResponse<IEnumerable<City>>> GetCities();
        Task<ApiResponse<IEnumerable<District>>> GetDistricts(int CityId);
    }
}