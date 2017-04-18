using System.Data.Entity;

namespace FAS.DAL
{
    internal class DbInitializer : CreateDatabaseIfNotExists<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            // insert base values
        }
    }
}
