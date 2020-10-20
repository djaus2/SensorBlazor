
using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Iot.Device.Common;
using Iot.Device.DHTxx;
using Iot.Device.Sht3x;

namespace Iot.Device.Samples
{
    /// <summary>
    /// Sensor: Sht3x
    /// Target is a RPi running Raspian with .NET Core 3.1
    /// This project is based upon the app: https://getiotsamples.azurewebsites.net/device/Sht3x-Sht3x.Samples
    /// This file is based upon: https://getiotsamples.azurewebsites.net/cssource (Changed from an app to a lib class)
    /// The circuit: https://github.com/dotnet/iot/tree/master/src/devices/Sht3x/samples
    /// The Project file there to use is: https://getiotsamples.azurewebsites.net/projectfileuse
    /// 
    /// Samples at getiotsamples are just the github dotnet/iot samples "rebadeged"
    /// Ref: A blog post about getiotsamples: https://davidjones.sportronics.com.au/blazor/Blazor-A_Wasm_app_for_presenting_Sample_Apps_from_an_API_Repository-coding.html
    /// </summary>
    public class Sht3x
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
        public static async Task Run(int num)
        {
            const int busId = 1;

            I2cConnectionSettings settings = new I2cConnectionSettings(busId, (byte)Iot.Device.Sht3x.I2cAddress.AddrLow);
            I2cDevice device = I2cDevice.Create(settings);

            List<double> values = null;

            Console.WriteLine("Hello Sht3x!");

            LastValues = new List<double>(3);
            using (Iot.Device.Sht3x.Sht3x sht3x = new Iot.Device.Sht3x.Sht3x(device))
            {
                Monitor.Enter(LastValues);
                ValuesQ = new Queue<List<double>>();
                keepRunning = true;
                int count = 0;
                while ((keepRunning) && (count < num))
                {
                    count++;
                    Monitor.Exit(LastValues);
                    var tempValue = sht3x.Temperature.Celsius;
                    var humValue = sht3x.Humidity;

                    values = new List<double> { tempValue, humValue, 0 };
                    ValuesQ.Enqueue(values);
                    Console.WriteLine($"Temperature: {tempValue:0.#}\u00B0C");
                    Console.WriteLine($"Relative humidity: {humValue:0.#}%");

                    await Task.Delay(2500);
                    Monitor.Enter(LastValues);
                }
                Monitor.Exit(LastValues);

            }
        }
    }
}
