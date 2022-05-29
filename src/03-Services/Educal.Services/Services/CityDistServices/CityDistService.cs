using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Contract.Responses;
using Educal.Core.Models;
using Educal.Database.Repositories.CityDistRepositories;
using Microsoft.Extensions.Logging;

namespace Educal.Services.Services.CityDistServices
{
    public class CityDistService : ICityDistService
    {
        private readonly ICityDistRepository _repo;
        private readonly ILogger<CityDistService> _logger;

        public CityDistService(ICityDistRepository repo, ILogger<CityDistService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<ApiResponse<IEnumerable<City>>> GetCities()
        {
            try
            {
                var result = await _repo.GetCities();
                return ApiResponse<IEnumerable<City>>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<IEnumerable<City>>.Fail(ex.Message,500);
            }
        }

        public async Task<ApiResponse<IEnumerable<District>>> GetDistricts(int CityId)
        {
            try
            {
                if(CityId == 0 || CityId == null ){
                    return ApiResponse<IEnumerable<District>>.Fail("Enter a valid CityID",400);
                }
                var result = await _repo.GetDistricts(CityId);

                if(result == null || !result.Any()){
                    return ApiResponse<IEnumerable<District>>.Fail("Enter a valid CityID",400);
                }

                return ApiResponse<IEnumerable<District>>.Success(result,200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return ApiResponse<IEnumerable<District>>.Fail(ex.Message,500);
            }
        }
    }
}