using ApiFirst;
using ApiFirst.Controllers;
using ApiFirst.Proto;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiFirst.Proto.WeatherFirst;

namespace ApiFirst.Grpc
{
    public class WeatherFirstService : WeatherFirstBase
    {

        private readonly ILogger _logger;
        public WeatherFirstService(ILogger<WeatherFirstService> logger)
        {
            _logger = logger;
        }

        private static readonly string[] Summaries = new[]
{
            "Soguk", "BuzGibi","Sıcak"
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
