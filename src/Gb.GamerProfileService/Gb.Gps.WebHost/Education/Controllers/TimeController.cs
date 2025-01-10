using Microsoft.AspNetCore.Mvc;

namespace Education.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController : ControllerBase
    {
        private readonly ILogger<TimeController> _logger;

        public TimeController(ILogger<TimeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("time")]
        [ResponseCache(Duration = 10)]
        public string Get()
        {
            return DateTime.Now.ToString("f");
        }

        [HttpGet("error")]
        public async Task<int> ThrowTask()
        {
            throw new Exception("NULL reference exception");
        }
    }
}