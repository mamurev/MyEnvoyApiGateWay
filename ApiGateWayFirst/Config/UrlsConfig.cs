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
            public static string GetItems() => $"/api/v1/WeatherFirst";
        }

        public class WheatherOperationSecond
        {
            public static string Get() => $"/api/v1/Weather";
        }

        public string WeatherFirst { get; set; }
        public string WeatherSecond { get; set; }
        

    }
}
