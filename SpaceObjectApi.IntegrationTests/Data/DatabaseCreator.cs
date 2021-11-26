using Microsoft.EntityFrameworkCore;
using SpaceObjectApi.Models;

namespace SpaceObjectApi.IntegrationTests.Data
{
    public class DatabaseCreator
    {
        public SpaceObjectContext Context { get; }

        public DatabaseCreator()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SpaceObjectContext>();

            var dbCreated = optionsBuilder.UseSqlite(@"Data Source=test.db").Options;

            Context = new SpaceObjectContext(dbCreated);
        }
    }
}
