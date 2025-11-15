using Microsoft.AspNetCore.Mvc;
using SmartCertify.Infrastructure;

namespace SmartCertify.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly SmartCertifyDbContext _smartCertifyDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            SmartCertifyDbContext smartCertifyDbContext)
        {
            _logger = logger;
            _smartCertifyDbContext = smartCertifyDbContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();

            var model = _smartCertifyDbContext.UserProfiles.ToList();
            return Ok(model);
        }
    }
}
