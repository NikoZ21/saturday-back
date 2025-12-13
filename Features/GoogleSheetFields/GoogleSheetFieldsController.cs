using Microsoft.AspNetCore.Mvc;
using Saturday_Back.Features.GoogleSheetFields.Dtos;

namespace Saturday_Back.Features.GoogleSheetFields
{
    [Route("api/google-sheet-fields")]
    [ApiController]
    public class GoogleSheetFieldsController(GoogleSheetFieldsService service) : ControllerBase
    {
        private readonly GoogleSheetFieldsService _service = service;

        [HttpGet]
        public async Task<ActionResult<GoogleSheetFieldsResponse>> GetAll()
        {
            var fields = await _service.GetAllAsync();
            return Ok(new { data = fields });
        }
    }
}