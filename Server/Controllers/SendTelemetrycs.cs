using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using BlazorSensorApp.Shared;
using System.Threading;

namespace BlazorSensorApp.Server.Controllers
{
    // Copyright (c) Microsoft. All rights reserved.
    // Licensed under the MIT license. See LICENSE file in the project root for full license information.

    // This application uses the Azure IoT Hub device SDK for .NET
    // For samples see: https://github.com/Azure/azure-iot-sdk-csharp/tree/master/iothub/device/samples


    public class SimulatedDeviceCS
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
        public  async Task StartSendDeviceToCloudMessageAsync(Sensor Sensor)
        {
            System.Diagnostics.Debug.WriteLine("===== SendDeviceToCloudMessageAsync In =====");
            var messageString = JsonConvert.SerializeObject(Sensor);
            var message = new Message(Encoding.ASCII.GetBytes(messageString));

            // Add a custom application property to the message.
            // An IoT hub can filter on these properties without access to the message body.
            //message.Properties.Add("temperatureAlert", (currentTemperature > 30) ? "true" : "false");

            // Send the telemetry message
            System.Diagnostics.Debug.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
            await s_deviceClient.SendEventAsync(message);
            System.Diagnostics.Debug.WriteLine("{0} > Sent message: {1}", DateTime.Now, messageString);
        }

 
        public  SimulatedDeviceCS()
        {
            System.Diagnostics.Debug.WriteLine("===== Starting StartMessageSending =====");

            s_connectionString = Shared.AppSettings.evIOTHUB_DEVICE_CONN_STRING;

            System.Diagnostics.Debug.WriteLine("Code from IoT Hub Quickstarts from Azure IoT Hub SDK");
            System.Diagnostics.Debug.WriteLine("Using Env Var IOTHUB_DEVICE_CONN_STRING = " + s_connectionString);

            // Connect to the IoT hub using the MQTT protocol
            s_deviceClient = DeviceClient.CreateFromConnectionString(s_connectionString, TransportType.Mqtt);
            System.Diagnostics.Debug.WriteLine("===== Finished StartMessageSending =====");
        }
    }
        
 }
