using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorSensorApp.Shared
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    public class Sensor
    {
        public string Id { get; set; }
        public double? Value { get; set; }

        //public int TemperatureF => 32 + (int)(Value / 0.5556);

        public bool State {get; set;}
        public List<double>? Values { get; set; }
        public SensorType SensorType { get; set; }
        public long TimeStamp { get; set; }
    }

    public enum SensorType {temperature,pressure,humidity,luminosity,accelerometer,environment,sswitch}
}
