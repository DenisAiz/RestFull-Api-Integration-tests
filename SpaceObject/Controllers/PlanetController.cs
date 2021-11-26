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
    public class PlanetController : ControllerBase
    {
        private ISpaceObjectRepository<Planet> _spaceObjectRepository;

        public PlanetController(ISpaceObjectRepository<Planet> ispaceObjectRepository)
        {
            _spaceObjectRepository = ispaceObjectRepository;
        }

        [HttpGet]
        public Task<List<Planet>> Get()
        {
            return _spaceObjectRepository.Get().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetId(int id)
        {
            var planet = await _spaceObjectRepository.GetAsync(id);

            return planet == null ? NotFound() : Ok(planet);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Planet planet)
        {
            await _spaceObjectRepository.InsertAsync(planet);

            await _spaceObjectRepository.SaveAsync();

            return Ok(planet);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Planet planet)
        {
            var subject = await _spaceObjectRepository.UpdateAsync(planet);

            if (!subject)
            {
                return NotFound();
            }
            else
            {
                await _spaceObjectRepository.SaveAsync();
            }

            return Ok(planet);
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
