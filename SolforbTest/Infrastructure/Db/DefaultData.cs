using SolforbTest.Infrastructure.Db.Models;

namespace SolforbTest.Infrastructure.Db
{
    public class DefaultData
    {
        public static async void Seed(SolforbDbContext _db)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (!_db.Providers.Any())
                {
                    var provider1 = new Provider()
                    {
                        Name = "Elshad"
                    };
                    _db.Providers.Add(provider1);
                    var provider2 = new Provider()
                    {
                        Name = "Ilkin"
                    };
                    _db.Providers.Add(provider2);
                    await _db.SaveChangesAsync();
                };
                transaction.Commit();
            }
            catch (System.Exception e)
            {
                transaction.Rollback();
            }
        }
    }
}

