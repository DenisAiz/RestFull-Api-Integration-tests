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
    public class StarController : ControllerBase
    {
        private ISpaceObjectRepository<Star> _spaceObjectRepository;

        public StarController(ISpaceObjectRepository<Star> ispaceObjectRepository)
        {
            _spaceObjectRepository = ispaceObjectRepository;
        }

        [HttpGet]
        public Task<List<Star>> Get()
        {
            return _spaceObjectRepository.Get().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetId(int id)
        {
            var star = await _spaceObjectRepository.GetAsync(id);

            return star == null ? NotFound() : Ok(star);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Star star)
        {
            await _spaceObjectRepository.InsertAsync(star);

            await _spaceObjectRepository.SaveAsync();

            return Ok(star);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Star star)
        {
            var subject = await _spaceObjectRepository.UpdateAsync(star);

            if (!subject)
            {
                return NotFound();
            }
            else
            {
                await _spaceObjectRepository.SaveAsync();
            }

            return Ok(star);
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
