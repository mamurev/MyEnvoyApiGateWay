using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateWayFirst.Config
{
    public class UrlsConfig
    {

        public class WheatherOperationFirst
        {
            public static string GetItems() => $"/api/v1/WeatherForecast";
        }

        public class WheatherOperationSecond
        {
            public static string Get() => $"/api/v1/WeatherForecast";
        }

        public string WheatherFirst { get; set; }
        public string WeatherSecond { get; set; }
        

    }
}
