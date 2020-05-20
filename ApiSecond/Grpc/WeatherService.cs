using ApiSecond.Controllers;
using ApiSecond.Proto;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiSecond.Proto.Weather;

namespace ApiSecond.Grpc
{
    public class WeatherService : WeatherBase
    {

        private readonly ILogger _logger;
        public WeatherService(ILogger<WeatherService> logger)
        {
            _logger = logger;
        }

        private static readonly string[] Summaries = new[]
{
            "Freezing2", "Bracing22", "Chilly2", "Cool2", "Mild2", "Warm2", "Balmy2", "Hot2", "Sweltering2", "Scorching2"
        };


        public override async Task<WeatherItemResponseMultiple> Get(WeatherRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin grpc call WeatherService.Get");
            var rng = new Random();
            var weatherList = Enumerable.Range(1, 5).Select(index => new Weather
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToList();

            return MapToResponse(weatherList);

        }

        private WeatherItemResponseMultiple MapToResponse(List<Weather> items)
        {
            var response = new WeatherItemResponseMultiple();

            items.ForEach(i =>
            {
                response.Items.Add(new WeatherItemResponse
                {
                    Date = (int)i.Date.Ticks,
                    Summary = i.Summary,
                    TemperatureC = i.TemperatureC,
                    TemperatureF = i.TemperatureF
                });
            });

            return response;
        }

    }
}
