using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogStashExemple.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly Random _rng;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _rng = new Random();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
            => Enumerable.Range(1, 5)
            .Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = _rng.Next(-20, 55),
                Summary = Summaries[_rng.Next(Summaries.Length)]
            })
            .ToArray();

        [HttpPost]
        public IActionResult Post([FromBody] Payload command)
        {
            _logger.LogInformation("User named {@command} ", command);
            return Ok(new { isSuccess = true });
        }

        [HttpGet("sendcommandwithresult")]
        public IActionResult Get(string command)
        {
            _logger.LogInformation("User named {@command} ", command);
            return Ok(new { isSuccess = true });
        }

        [HttpPost("withresult")]
        public IActionResult PostWithResult([FromBody] Payload command)
        {
            _logger.LogInformation("User named {@command} ", command);
            return Ok(new { isSuccess = true, value = command });
        }
    }

    public class Payload
    {
        public string Name { get; set; }
        public string CPF { get; set; }
    }
}
