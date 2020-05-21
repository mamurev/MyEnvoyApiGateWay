using ApiGateWayFirst.Config;
using ApiGateWayFirst.Models;
using Microsoft.eShopOnContainers.Mobile.Shopping.HttpAggregator.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
//using ApiSecond.Proto;
using Grpc.Net.Client;
using ApiSecond.Proto;
using ApiFirst.Proto;

namespace ApiGateWayFirst.Services
{
    public class WeatherService : IWeatherService
    {

        private readonly HttpClient _httpClient;
        private readonly UrlsConfig _urls;
        private readonly ILogger<WeatherService> _logger;
        public WeatherService(HttpClient httpClient, IOptions<UrlsConfig> config, ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _urls = config.Value;
            _logger = logger;
        }

        public async Task<IEnumerable<WeatherDataItem>> GetSecond()
        {
            return await GrpcCallerService.CallService(_urls.WeatherSecond, async httpClient =>
            {
                var channel = GrpcChannel.ForAddress(_urls.WeatherSecond);
                var client = new Weather.WeatherClient(channel);
                _logger.LogDebug("grpc client created, request");
                var response = await client.GetAsync(new ApiSecond.Proto.WeatherRequest());
                _logger.LogDebug("grpc response {@response}", response);

                return MapToWeatherData(response);
            });
        }

        public async Task<IEnumerable<WeatherDataItem>> GetFirst()
        {
            return await GrpcCallerService.CallService(_urls.WeatherFirst, async httpClient =>
            {
                var channel = GrpcChannel.ForAddress(_urls.WeatherFirst);
                var client = new WeatherFirst.WeatherFirstClient(channel);
                _logger.LogDebug("grpc client created, request");
                var response = await client.GetAsync(new ApiFirst.Proto.WeatherRequest());
                _logger.LogDebug("grpc response {@response}", response);

                return MapToWeatherData(response);
            });
        }

        public async Task<IEnumerable<WeatherDataItem>> Get()
        {
            var resultFirst= await GetFirst();
            var resultSecond = await GetSecond();

            return resultFirst.Concat(resultSecond);
        }

        private List<WeatherDataItem> MapToWeatherData(ApiSecond.Proto.WeatherItemResponseMultiple weatherResponse)
        {
            if (weatherResponse == null)
            {
                return null;
            }

            var map = new WeatherData();

            weatherResponse.Items.ToList().ForEach(item => map.Items.Add(new WeatherDataItem
            {
                Date = DateTime.Now.AddTicks(item.Date),
                Summary = item.Summary,
                TemperatureC = item.TemperatureC,
                TemperatureF = item.TemperatureF
            }));

            return map.Items;
        }

        private List<WeatherDataItem> MapToWeatherData(ApiFirst.Proto.WeatherItemResponseMultiple weatherResponse)
        {
            if (weatherResponse == null)
            {
                return null;
            }

            var map = new WeatherData();

            weatherResponse.Items.ToList().ForEach(item => map.Items.Add(new WeatherDataItem
            {
                Date = DateTime.Now.AddTicks(item.Date),
                Summary = item.Summary,
                TemperatureC = item.TemperatureC,
                TemperatureF = item.TemperatureF
            }));

            return map.Items;
        }

    }
}
