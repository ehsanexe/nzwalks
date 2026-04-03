using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionDomain = await regionRepository.GetAllAsync();

            return Ok(mapper.Map<List<RegionDto>>(regionDomain));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);

            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
            var createdRegion = await regionRepository.CreateRegionAsync(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = createdRegion.Id }, mapper.Map<RegionDto>(createdRegion));
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
            return Ok(mapper.Map<RegionDto>(updatedRegion));
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
            return Ok(mapper.Map<RegionDto>(deletedRegion));
        }
    }
}
