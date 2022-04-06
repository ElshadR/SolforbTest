using SolforbTest.Infrastructure.Db.Models;

namespace SolforbTest.Infrastructure.Services.Contracts
{
    public interface IProviderService
    {
        Task<IEnumerable<Provider>> GetList();

    }
}
