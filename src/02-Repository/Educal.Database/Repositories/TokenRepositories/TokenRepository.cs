using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educal.Core.Models;
using Educal.Database.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Educal.Database.Repositories.TokenRepositories
{
    public class TokenRepository : BaseRepository<UserRefreshToken>, ITokenRepository
    {
        private readonly EducalDbContext _context;

        public TokenRepository(EducalDbContext context)
        {
            _context = context;
        }

        public override async Task AddAsync(UserRefreshToken entity)
        {
            await _context.RefreshTokens.AddAsync(entity);
        }

        public override async Task AddRangeAsync(IEnumerable<UserRefreshToken> entities)
        {
            await _context.RefreshTokens.AddRangeAsync(entities);
        }

        public override void Delete(UserRefreshToken entity)
        {
            _context.RefreshTokens.Remove(entity);
        }

        public async override Task<IEnumerable<UserRefreshToken>> GetAll()
        {
            return await _context.RefreshTokens.AsNoTracking().ToListAsync();
        }

        public override async Task<UserRefreshToken> GetByGuidAsync(Guid Id)
        {
            return await _context.RefreshTokens.Where(c => c.Guid == Id)
                                               .FirstOrDefaultAsync();
        }

        public override Task<UserRefreshToken> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(UserRefreshToken entity)
        {
            _context.RefreshTokens.Update(entity);
        }

        public override async Task<IQueryable<UserRefreshToken>> Where(Expression<Func<UserRefreshToken, bool>> expression)
        {
            return _context.RefreshTokens.Where(expression);
        }
    }
}