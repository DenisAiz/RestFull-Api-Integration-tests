using Microsoft.EntityFrameworkCore;
using SpaceObjectApi.Entities;

namespace SpaceObjectApi.Models
{
    public class SpaceObjectContext : DbContext
    {
        public DbSet<SpaceObject> SpaceObjects { get; set; }
        public DbSet<Asteroid> Asteroids { get; set; }
        public DbSet<BlackHole> BlackHoles { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Star> Stars { get; set; }

        public SpaceObjectContext(DbContextOptions<SpaceObjectContext> options) 
            : base(options) 
        {

        }   
    }
}

