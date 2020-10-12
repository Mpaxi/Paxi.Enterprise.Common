using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paxi.Enterprise.Common.WebApi.Model;
using Paxi.Enterprise.Common.WebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paxi.Enterprise.Common.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserRepository _userRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            try
            {
                IQueryable<User> users = _userRepository.Where(w => w.Active.Equals(true));
                User user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Nome"
                };

                _userRepository.Add(user);
                _userRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }



            Random rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
