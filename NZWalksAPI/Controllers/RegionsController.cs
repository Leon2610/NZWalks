using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await dbContext.Regions.ToListAsync();

            var regionsDTO = new List<RegionDTO>();
            foreach (var region in regions)
            {
                regionsDTO.Add(new RegionDTO
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = new RegionDTO()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            var regionDomainModel = new Region()
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };

            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();

            var regionDTO = new RegionDTO()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id =  regionDomainModel.Id }, regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var regionDomailModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomailModel == null)
            {
                return NotFound();
            }

            regionDomailModel.Code = updateRegionRequestDTO.Code;
            regionDomailModel.Name = updateRegionRequestDTO.Name;
            regionDomailModel.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            var regionDTO = new RegionDTO()
            {
                Id = regionDomailModel.Id,
                Code = regionDomailModel.Code,
                Name = regionDomailModel.Name,
                RegionImageUrl = regionDomailModel.RegionImageUrl
            };

            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomailModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomailModel == null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(regionDomailModel);
            await dbContext.SaveChangesAsync();

            var regionDTO = new RegionDTO()
            {
                Id = regionDomailModel.Id,
                Code = regionDomailModel.Code,
                Name = regionDomailModel.Name,
                RegionImageUrl = regionDomailModel.RegionImageUrl
            };

            return Ok(regionDTO);
        }
    }
}
