
using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Threading;
using System.Threading.Tasks;
using Iot.Device.Bmxx80;
//using Iot.Device.Bmxx80.FilteringMode;
using Iot.Device.Bmxx80.PowerMode;
using Iot.Device.Common;
//using UnitsNet;

namespace Iot.Device.Samples
{
    /// <summary>
    /// Sensor BME280
    /// Target is a RPi running Raspian with .NET Core 3.1
    /// This project is based upon the app: https://getiotsamples.azurewebsites.net/device/Bmxx80-Bme280.sample
    /// This file is based upon: https://getiotsamples.azurewebsites.net/cssource (Changed from an app to a lib class)
    /// This has been simplified so that the published version of IoT.Device.Bidinings V 1.0.0 can be used.
    /// The circuit is at https://getiotsamples.azurewebsites.net/circuit
    /// The Project file there to use is: https://getiotsamples.azurewebsites.net/projectfileuse
    /// 
    /// Samples at getiotsamples are just the github dotnet/iot samples "rebadeged"
    /// Ref: A blog post about getiotsamples: https://davidjones.sportronics.com.au/blazor/Blazor-A_Wasm_app_for_presenting_Sample_Apps_from_an_API_Repository-coding.html
    /// </summary>
    public class BME280Sensor
    {
        public static async Task<List<double>> Read()
        {
            Console.WriteLine("Hello Bme280!");

            // bus id on the raspberry pi 3
            const int busId = 1;

            var i2cSettings = new I2cConnectionSettings(busId, Bme280.DefaultI2cAddress);
            var i2cDevice = I2cDevice.Create(i2cSettings);
            Bme280 i2CBmpe80 = new Bme280(i2cDevice);

            List<double> values = null;

            using (i2CBmpe80)
            {

                // set mode forced so device sleeps after read
                i2CBmpe80.SetPowerMode(Bmx280PowerMode.Forced);

                var tempV = await i2CBmpe80.ReadTemperatureAsync();
                var tempValue = tempV.Celsius;
                var preValue = await i2CBmpe80.ReadPressureAsync();
                var humValue = await i2CBmpe80.ReadHumidityAsync();;


                Console.WriteLine($"Temperature: {tempValue:0.#}\u00B0C");
                Console.WriteLine($"Pressure: {preValue:0.##}hPa");
                Console.WriteLine($"Relative humidity: {humValue:0.#}%");


                values = new List<double> { tempValue, humValue, preValue };
            }
            return values;
        }
    }
}
