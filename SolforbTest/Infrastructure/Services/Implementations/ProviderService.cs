using Microsoft.EntityFrameworkCore;
using SolforbTest.Infrastructure.Db;
using SolforbTest.Infrastructure.Db.Models;
using SolforbTest.Infrastructure.Services.Contracts;

namespace SolforbTest.Infrastructure.Services.Implementations
{
    public class ProviderService : IProviderService
    {
        private SolforbDbContext _db { get; set; }

        public ProviderService(SolforbDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Provider>> GetList()
        {
            var providers = await _db.Providers
               .ToListAsync();

            return providers;
        }
    }
}
