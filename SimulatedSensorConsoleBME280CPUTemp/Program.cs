//using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BlazorSensorApp.Shared;
using Microsoft.Extensions.Configuration;
using Iot.Device.Samples;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main()
        {
            //Console.WriteLine("Hello Sensor! Press [Enter] when service is running.");
            //Console.ReadLine();
            

            var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                           //.AddUserSecrets<Program>()
                           .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();
            var mySettingsConfig = new AppSettings();
            configuration.GetSection("AppSettings").Bind(mySettingsConfig);

            string url = $"{ mySettingsConfig.Url}/{ mySettingsConfig.Api}";

            int numToSend = mySettingsConfig.NumToSend;
            Console.WriteLine("Sending {0} messages", numToSend);

            List<double> values = null;
            double value = double.NaN;

            Task[] myTasks = new Task[numToSend];
            for (int i=0;i<myTasks.Length;i++)
            {
                value = await CUPCoreTemp.Read();
                myTasks[i] = Send(i, url, SensorType.temperature, value, values);
                await Task.Delay(1000);
            }

            Task.WaitAll(myTasks);
            Console.WriteLine("Hello Sensor! End");

        }

        static int count = 0;
        static async Task Send(int No,string url, SensorType sensorType, Double value, List<Double> values)
        {
            try
            {
                System.Net.Http.HttpClient httpClient = new HttpClient();
                Guid guid = Guid.NewGuid();
                long TimeStamp = DateTime.Now.Ticks;
                Sensor _Sensor = new Sensor();
                _Sensor.No = No;
                _Sensor.Id = guid.ToString();
                _Sensor.SensorType = sensorType;
                _Sensor.TimeStamp = TimeStamp;
                _Sensor.Value = value;
                _Sensor.Values = values;

                Console.WriteLine($"{ _Sensor.SensorType}: { _Sensor.Value}");

                var dataAsString = JsonConvert.SerializeObject(_Sensor);
                var content = new StringContent(dataAsString);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var resultPost = await httpClient.PostAsync(url, content);

                Console.WriteLine(resultPost.StatusCode.ToString());
                var sr = await resultPost.Content.ReadAsStringAsync();
                Console.WriteLine(sr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public  class AppSettings{
            public string Url { get; set; }
            public string Api { get; set; }

            public int NumToSend { get; set; }
        }

    }

}
