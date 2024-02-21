using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {


        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Auckland Region",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/16731827/pexels-photo-16731827/free-photo-of-mar-amanecer-paisaje-puesta-de-sol.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Wellington Region",
                    Code = "WGL",
                    RegionImageUrl = "https://images.pexels.com/photos/19517655/pexels-photo-19517655/free-photo-of-ligero-mar-amanecer-paisaje.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                }
            };
            return Ok(regions);
        }
    }
}
