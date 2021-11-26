using SpaceObjectApi.Entities;
using SpaceObjectApi.Models;
using System.Collections.Generic;

namespace SpaceObjectApi.IntegrationTests.Data
{
    public static class Utilities
    {
        public static void InitializeDbForTests(SpaceObjectContext db)
        {
            db.SpaceObjects.AddRange(OnModelCreating());
            db.Database.EnsureCreated();
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(SpaceObjectContext db)
        {
            db.SpaceObjects.RemoveRange(db.SpaceObjects);
            db.SaveChanges();
        }

        public static List<SpaceObject> OnModelCreating()
        {
            return new List<SpaceObject>()
            {
                new Asteroid
                {
                    Name = "",
                    Description = "",
                    Weight = ""
                },
                new BlackHole
                {
                    Name = "",
                    Description = "",
                    Diameter = ""
                },
                new Planet
                {
                    Name = "",
                    Description = "",
                    DistanceFromEarth = ""
                },
                new Star
                {
                    Name = "",
                    Description = "",
                    NumberOfYears = ""
                }
            };
        }
    }
}
