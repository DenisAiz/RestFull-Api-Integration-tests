using SpaceObjectApi.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceObjectApi.Repository
{
    public interface ISpaceObjectRepository<T>
        where T : SpaceObject
    {
        IQueryable<T> Get();

        Task<T> GetAsync(int id);

        Task InsertAsync(T spaceObject);

        Task<bool> DeleteAsync(int objectId);

        Task<bool> UpdateAsync(T spaceObject);

        Task SaveAsync();
    }
}
