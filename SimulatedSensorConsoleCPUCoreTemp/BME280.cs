
using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Threading;
using System.Threading.Tasks;
using Iot.Device.Bmxx80;
//using Iot.Device.Bmxx80.FilteringMode;
using Iot.Device.Bmxx80.PowerMode;
using Iot.Device.Common;
using Iot.Device.CpuTemperature;

namespace Iot.Device.Samples
{
    /// <summary>
    /// Requires no hardware/connections. Target is a RPi running Raspian with .NET Core 3.1
    /// This project is based upon the app: https://getiotsamples.azurewebsites.net/device/CpuTemperature-CpuTemperature.Samples
    /// This file is based upon: https://getiotsamples.azurewebsites.net/cssource (Changed from an app to a lib class)
    /// The circuit: This requires no circuitry
    /// The Project file there to use is: https://getiotsamples.azurewebsites.net/projectfileuse
    /// 
    /// Samples at getiotsamples are just the github dotnet/iot samples "rebadeged"
    /// Ref: A blog post about getiotsamples: https://davidjones.sportronics.com.au/blazor/Blazor-A_Wasm_app_for_presenting_Sample_Apps_from_an_API_Repository-coding.html
    /// </summary>
    public class CUPCoreTemp
    {
        public static async Task<double> Read()
        {
            Iot.Device.CpuTemperature.CpuTemperature  cpuTemperature = new Iot.Device.CpuTemperature.CpuTemperature();

            double temperature = double.NaN;
            if (cpuTemperature.IsAvailable)
            {
                temperature = cpuTemperature.Temperature.Celsius;
                if (!double.IsNaN(temperature))
                {
                    Console.WriteLine($"CPU Temperature: {temperature} C");
                }
            }
            return temperature;
        }
    }
}
