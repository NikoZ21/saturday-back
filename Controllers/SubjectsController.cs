using Microsoft.AspNetCore.Mvc;
using Saturday_Back.Dtos;
using Saturday_Back.Entities;
using Saturday_Back.Repositories;
using Saturday_Back.Services;

namespace Saturday_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController(ICachedRepository<Subject, SubjectResponseDto> repository) : ControllerBase
    {
        private readonly ICachedRepository<Subject, SubjectResponseDto> _repository = repository;
    
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var subjects = await _repository.GetAllAsync();
            return Ok(subjects);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Subject subject)
        {
            await _repository.AddAsync(subject);
            return Ok("Subject created and cache refreshed");
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Subject subject)
        {
            await _repository.UpdateAsync(subject);
            return Ok("Subject updated and cache refreshed");
        }       
    }
}
