namespace SpaceObjectApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SpaceObjectApi.Entities;
    using SpaceObjectApi.Repository;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class AsteroidController : ControllerBase
    {
        private ISpaceObjectRepository<Asteroid> _spaceObjectRepository;

        public AsteroidController(ISpaceObjectRepository<Asteroid> spaceObjectRepository)
        {
            _spaceObjectRepository = spaceObjectRepository;
        }

        [HttpGet]
        public Task<List<Asteroid>> Get()
        {
            return _spaceObjectRepository.Get().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetId(int id)
        {
            var asteroid = await _spaceObjectRepository.GetAsync(id);

            return asteroid == null ? NotFound() : Ok(asteroid);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Asteroid asteroid)
        {
            await _spaceObjectRepository.InsertAsync(asteroid);

            await _spaceObjectRepository.SaveAsync();

            return Ok(asteroid);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Asteroid asteroid)
        {
            var subject = await _spaceObjectRepository.UpdateAsync(asteroid);

            if (!subject)
            {
                return NotFound();
            }
            else
            {
                await _spaceObjectRepository.SaveAsync();
            }

            return Ok(asteroid);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var subject = await _spaceObjectRepository.DeleteAsync(id);

            if (!subject)
            {
                return NotFound();
            }
            else
            {
                await _spaceObjectRepository.SaveAsync();
            }

            return Ok(subject);
        }
    }
}
