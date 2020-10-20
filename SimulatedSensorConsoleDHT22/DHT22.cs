
using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Iot.Device.Common;
using Iot.Device.DHTxx;

namespace Iot.Device.Samples
{
    /// <summary>
    /// Requires no hardware/connections. Target is a RPi running Raspian with .NET Core 3.1
    /// This project is based upon the app: https://getiotsamples.azurewebsites.net/device/Dhtxx-DhtSensor.sample
    /// This file is based upon: https://getiotsamples.azurewebsites.net/cssource (Changed from an app to a lib class)
    /// The circuit: Connect OneWitePin to RPi pin 26
    /// The Project file there to use is: https://getiotsamples.azurewebsites.net/projectfileuse
    /// 
    /// Samples at getiotsamples are just the github dotnet/iot samples "rebadeged"
    /// Ref: A blog post about getiotsamples: https://davidjones.sportronics.com.au/blazor/Blazor-A_Wasm_app_for_presenting_Sample_Apps_from_an_API_Repository-coding.html
    /// </summary>
    public class DHT22
    {
        static List<double> LastValues = null;
        public static List<double> Read()
        {
            List<double> values = null;
            if (ValuesQ.Count != 0)
            {
                values  = ValuesQ.Dequeue();
            }
            return values;
        }

        public static void Stop()
        {
            Monitor.Enter(LastValues);
            keepRunning = false;
            Monitor.Exit(LastValues);
        }

        public static  bool keepRunning = false;
        public static Queue<List<double>> ValuesQ = new Queue<List<double>>();
        public static async Task Run()
        {
            int oneWirePin = 26;

            List<double> values = null;

            Console.WriteLine("Hello DHT!");

            LastValues = new List<double>(3);
            using (Dht22 dht = new Dht22(oneWirePin))
            {
                Monitor.Enter(LastValues);
                ValuesQ = new Queue<List<double>>();
                keepRunning = true;
                while (keepRunning)
                {
                    Monitor.Exit(LastValues);                   
                    var tempValue = dht.Temperature.Celsius;
                    bool result1 = dht.IsLastReadSuccessful;
                    var humValue = dht.Humidity;
                    bool result2 = dht.IsLastReadSuccessful;

                    if (result1 && result2)
                    {
                        values = new List<double> { tempValue, humValue, 0 };
                        ValuesQ.Enqueue(values);
                        Console.WriteLine($"Temperature: {tempValue:0.#}\u00B0C");
                        Console.WriteLine($"Relative humidity: {humValue:0.#}%");
                    }
                    else
                    {
                        Console.WriteLine("Last read not successful");
                    }
                    await Task.Delay(2500);
                    Monitor.Enter(LastValues);
                }
                Monitor.Exit(LastValues);

            }
        }
    }
}
