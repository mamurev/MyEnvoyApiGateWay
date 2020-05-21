using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateWayFirst.Models;
using ApiGateWayFirst.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiGateWayFirst.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AggController : ControllerBase
    {
        private readonly ILogger<AggController> _logger;
        private readonly IWeatherService _weatherService;

        public AggController(ILogger<AggController> logger,IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;

        }

        [HttpGet]
        public Task <IEnumerable<WeatherDataItem>> Get()
        {
           return  _weatherService.Get();
        }
    }
}
