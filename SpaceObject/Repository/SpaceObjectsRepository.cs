using Microsoft.EntityFrameworkCore;
using SpaceObjectApi.Entities;
using SpaceObjectApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceObjectApi.Repository
{
    public class SpaceObjectRepository<T> : ISpaceObjectRepository<T> where T : SpaceObject
    {
        private SpaceObjectContext _context;

        public SpaceObjectRepository(SpaceObjectContext context)
        {
            _context = context;
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>();
        }

        public Task<T> GetAsync(int id)
        {
            return _context.FindAsync<T>(id).AsTask();
        }

        public Task InsertAsync(T spaceObject)
        {
             return _context.SpaceObjects.AddAsync(spaceObject).AsTask();
        }

        public async Task<bool> DeleteAsync(int objectId)
        {
            var spaceObject = await _context.SpaceObjects.FirstOrDefaultAsync(a => a.Id == objectId);

            if (spaceObject == null)
            {
                return false;
            }
            else
            {
                _context.SpaceObjects.Remove(spaceObject);
            }

            return true;
        }

        public async Task<bool> UpdateAsync(T spaceObject)
        {
            var obj = await _context.SpaceObjects.FirstOrDefaultAsync(a => a.Id == spaceObject.Id);

            if (obj == null)
            {
                return false;
            }
            else
            {
                _context.Entry(obj).CurrentValues.SetValues(spaceObject);
            }

            return true;
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
