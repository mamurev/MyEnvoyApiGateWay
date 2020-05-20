using ApiGateWayFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateWayFirst.Services
{
  public  interface IWeatherService
    {

        Task<IEnumerable<WeatherDataItem>> Get();

       
    }
}
