using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASP.NETCore_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new List<string>
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Count)]
            })
            .ToArray();
        }

        [HttpGet("all")]
        public IEnumerable<string> GetAll()
        {
            return Summaries;
        }

        [HttpPost("add")]
        public IEnumerable<string> Add([FromBody] string value)
        {
            Summaries.Add(value);
            return Summaries;
        }

        [HttpPut("update/{oldValue}")]
        public IEnumerable<string> Update(string oldValue,[FromBody]string newValue)
        {
            Summaries = Summaries.Select(x => x == oldValue ? newValue : x).ToList();
            return Summaries;
        }


        [HttpDelete("delete/{value}")]
        public IEnumerable<string> Delete(string value)
        {
            Summaries = Summaries.Where(x => !x.Contains(value)).ToList();
            return Summaries;
        }
    }
}
