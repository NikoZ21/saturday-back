using Microsoft.AspNetCore.Mvc;
namespace Saturday_Back.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var configs = new Dictionary<string, string>
            {
                ["DevelopmentDb"] = _configuration.GetConnectionString("DevelopmentDb") ?? "Not found",
                ["SomeSetting"] = _configuration["SomeSetting"] ?? "Not found",
                ["Logging:LogLevel:Default"] = _configuration["Logging:Log,Level:Default"] ?? "Not found",
                ["Serilog:Using:0"] = _configuration["Serilog:Using:0"] ?? "Not found"
            };

            return Ok(configs);
        }
    }
}