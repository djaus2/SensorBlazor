
using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Iot.Device.Common;
using Iot.Device.Ads1115;

namespace Iot.Device.Samples
{
    /// <summary>
    /// Sensor: ADS1115 ADC Convertor
    /// Target is a RPi running Raspian with .NET Core 3.1
    /// This project is based upon the app: https://getiotsamples.azurewebsites.net/device/Ads1115-Ads1115.Samples
    /// This file is based upon: https://getiotsamples.azurewebsites.net/cssource (Changed from an app to a lib class)
    /// The circuit: https://getiotsamples.azurewebsites.net/circuit
    /// The Project file there to use is: https://getiotsamples.azurewebsites.net/projectfileuse
    /// 
    /// Samples at getiotsamples are just the github dotnet/iot samples "rebadeged"
    /// Ref: A blog post about getiotsamples: https://davidjones.sportronics.com.au/blazor/Blazor-A_Wasm_app_for_presenting_Sample_Apps_from_an_API_Repository-coding.html
    /// </summary>
    public class AD115_ADC
    {
        static List<double> LastValues = null;
        public static double Read()
        {
            double value = double.NaN;
            if (ValuesQ.Count != 0)
            {
                value  = ValuesQ.Dequeue();
            }
            return value;
        }

        public static void Stop()
        {
            Monitor.Enter(LastValues);
            keepRunning = false;
            Monitor.Exit(LastValues);
        }

        public static  bool keepRunning = false;
        public static Queue<double> ValuesQ = new Queue<double>();
        public static async Task Run(int num)
        {
            // set I2C bus ID: 1
            // ADS1115 Addr Pin connect to GND
            I2cConnectionSettings settings = new I2cConnectionSettings(1, (int)I2cAddress.GND);
            // get I2cDevice (in Linux)
            I2cDevice device = I2cDevice.Create(settings);

            List<double> values = null;

            Console.WriteLine("Hello Sht3x!");

            LastValues = new List<double>(3);
            using (Iot.Device.Ads1115.Ads1115 adc = new Iot.Device.Ads1115.Ads1115(device, InputMultiplexer.AIN0, MeasuringRange.FS4096))
            {
                Monitor.Enter(LastValues);
                ValuesQ = new Queue<double>();
                keepRunning = true;
                int count = 0;
                while ((keepRunning) && (count < num))
                {
                    count++;
                    Monitor.Exit(LastValues);
                    // read raw data form the sensor
                    short raw = adc.ReadRaw();
                    // raw data convert to voltage
                    double voltage = adc.RawToVoltage(raw);

                    Console.WriteLine($"ADS1115 Raw Data: {raw}");
                    Console.WriteLine($"Voltage: {voltage}");

                    ValuesQ.Enqueue(voltage);

                    await Task.Delay(2500);
                    Monitor.Enter(LastValues);
                }
                Monitor.Exit(LastValues);
            }
        }
    }
}
