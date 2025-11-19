using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saturday_Back.Entities;
using Saturday_Back.Services;

namespace Saturday_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseCostController(BaseCostService baseCostService) : ControllerBase
    {
      private readonly BaseCostService _baseCostService = baseCostService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var BaseCosts = await _baseCostService.GetAllAsync();
            return Ok(BaseCosts);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BaseCost BaseCost)
        {
            await _baseCostService.AddAsync(BaseCost);
            return Ok("BaseCost created and cache refreshed");
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] BaseCost BaseCost)
        {
            await _baseCostService.UpdateAsync(BaseCost);
            return Ok("BaseCost updated and cache refreshed");
        }
    }
}
