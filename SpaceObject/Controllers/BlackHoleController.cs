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
    public class BlackHoleController : ControllerBase
    {
        private ISpaceObjectRepository<BlackHole> _spaceObjectRepository;

        public BlackHoleController(ISpaceObjectRepository<BlackHole> ispaceObjectRepository)
        {
            _spaceObjectRepository = ispaceObjectRepository;
        }

        [HttpGet]
        public Task<List<BlackHole>> Get()
        {
            return _spaceObjectRepository.Get().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetId(int id)
        {
            var blackHole = await _spaceObjectRepository.GetAsync(id);

            return blackHole == null ? NotFound() : Ok(blackHole);
        }

        [HttpPost]
        public async Task<ActionResult> Create(BlackHole blackHole)
        {
            await _spaceObjectRepository.InsertAsync(blackHole);

            await _spaceObjectRepository.SaveAsync();

            return Ok(blackHole);
        }

        [HttpPut]
        public async Task<ActionResult> Update(BlackHole blackHole)
        {
            var subject = await _spaceObjectRepository.UpdateAsync(blackHole);

            if (!subject)
            {
                return NotFound();
            }
            else
            {
                await _spaceObjectRepository.SaveAsync();
            }

            return Ok(blackHole);
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
