using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateWayFirst.Models
{
    public class WeatherData
    {


        public List<WeatherDataItem> Items { get; set; } = new List<WeatherDataItem>();

        public WeatherData()
        {

        }
        //public string WheaterId { get; set; }

        //public WeatherData(string wheaterId)
        //{
        //    WheaterId = wheaterId;
        //}
    }

    public class WeatherDataItem
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }

        public string Summary { get; set; }


    }
}
