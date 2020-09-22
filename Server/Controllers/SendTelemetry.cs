using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using BlazorSensorApp.Shared;

namespace BlazorSensorApp.Server.Controllers
{
    // Copyright (c) Microsoft. All rights reserved.
    // Licensed under the MIT license. See LICENSE file in the project root for full license information.

    // This application uses the Azure IoT Hub device SDK for .NET
    // For samples see: https://github.com/Azure/azure-iot-sdk-csharp/tree/master/iothub/device/samples


        public static class SimulatedDevice
        {
            private static DeviceClient s_deviceClient;

        // The device connection string to authenticate the device with your IoT hub.
        // Using the Azure CLI:
        // az iot hub device-identity show-connection-string --hub-name {YourIoTHubName} --device-id MyDotnetDevice --output table
        ////private readonly static string s_connectionString = "{Your device connection string here}";

        // For this sample either
        // - pass this value as a command-prompt argument
        // - set the IOTHUB_DEVICE_CONN_STRING environment variable 
        // - create a launchSettings.json (see launchSettings.json.template) containing the variable
        private static string s_connectionString = Environment.GetEnvironmentVariable("IOTHUB_DEVICE_CONN_STRING");



            // Async method to send simulated telemetry
            private static async void SendDeviceToCloudMessageAsync(Sensor telemetryDataPoint)
            {

                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                // Add a custom application property to the message.
                // An IoT hub can filter on these properties without access to the message body.
                //message.Properties.Add("temperatureAlert", (currentTemperature > 30) ? "true" : "false");

                // Send the telemetry message
                await s_deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);            
            }
            public static void Main(Sensor sensor)
            {
                Console.WriteLine("IoT Hub Quickstarts #1 - Simulated device. Ctrl-C to exit.\n");
                Console.WriteLine("Using Env Var IOTHUB_DEVICE_CONN_STRING = " + s_connectionString);

                // Connect to the IoT hub using the MQTT protocol
                s_deviceClient = DeviceClient.CreateFromConnectionString(s_connectionString, TransportType.Mqtt);
                SendDeviceToCloudMessageAsync(sensor);
            }
        }
 }
