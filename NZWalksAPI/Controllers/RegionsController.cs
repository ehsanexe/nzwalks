using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;

        public RegionsController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionDomain = await regionRepository.GetAllAsync();
            return Ok("This will return all regions");
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);

            return Ok("This will return a region with id " + id);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Region region)
        {
            var createdRegion = await regionRepository.CreateRegionAsync(region);
            return Ok(createdRegion);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Region region)
        {
            var updatedRegion = await regionRepository.UpdateRegionAsync(id, region);
            if (updatedRegion == null)
            {
                return NotFound();
            }
            return Ok(updatedRegion);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedRegion = await regionRepository.DeleteAsync(id);
            if (deletedRegion == null)
            {
                return NotFound();
            }
            return Ok(deletedRegion);

        }
    }
}
