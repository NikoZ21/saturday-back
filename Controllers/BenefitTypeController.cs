using Microsoft.AspNetCore.Mvc;
using Saturday_Back.Entities;
using Saturday_Back.Services;

namespace Saturday_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenefitTypeController : Controller
    {
        private readonly BenefitTypeService _benefitTypeService;
        public BenefitTypeController(BenefitTypeService benefitTypeService)
        {
            _benefitTypeService = benefitTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var benefitTypes = await _benefitTypeService.GetAllAsync();
            return Ok(benefitTypes);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] BenefitType benefitType)
        {
            await _benefitTypeService.UpdateBenefitTypeAsync(benefitType);
            return Ok("Payment type updated and cache refreshed");
        }
    }
}
