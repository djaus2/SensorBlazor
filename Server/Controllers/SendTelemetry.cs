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
using System.Threading;

namespace BlazorSensorApp.Server.Controllers
{
    // Copyright (c) Microsoft. All rights reserved.
    // Licensed under the MIT license. See LICENSE file in the project root for full license information.

    // This application uses the Azure IoT Hub device SDK for .NET
    // For samples see: https://github.com/Azure/azure-iot-sdk-csharp/tree/master/iothub/device/samples


    public static class SimulatedDevice
    {
        public static bool KeepRunning { get; set; } = false;
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

        public static void SendMessage(Sensor _Sensor)
        {
            Sensor = _Sensor;
            SensorEvent.Set();
        }

        public static void StopMessageSending()
        {
            KeepRunning = false;
            SensorEvent.Set();
        }

        private static Sensor Sensor;

        // Async method to send simulated telemetry
        private static async Task StartSendDeviceToCloudMessageAsync()
        {
            System.Diagnostics.Debug.WriteLine("===== Starting waiting for Messages =====");
            while (KeepRunning)
            {
                System.Diagnostics.Debug.WriteLine("===== Waiting =====");
                SensorEvent.WaitOne();
                System.Diagnostics.Debug.WriteLine("===== Done Waiting =====");
                if (KeepRunning)
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
                    System.Diagnostics.Debug.WriteLine("===== SendDeviceToCloudMessageAsync Out ======");
                }
            }
            System.Diagnostics.Debug.WriteLine("===== Finished waiting for Messages =====");
        }

        private static AutoResetEvent SensorEvent;
        public static async Task StartMessageSending()
        {
            System.Diagnostics.Debug.WriteLine("===== Starting StartMessageSending =====");
            KeepRunning = true;
            SensorEvent = new AutoResetEvent(false);

            s_connectionString = "HostName=BlazeMe3.azure-devices.net;DeviceId=DevBlaze;SharedAccessKey=5FAHd4pecYnX7P9Y7XclgPV+QeFB4s41l6xykR81dnE=";

            System.Diagnostics.Debug.WriteLine("Code from IoT Hub Quickstarts from Azure IoT Hub SDK");
            System.Diagnostics.Debug.WriteLine("Using Env Var IOTHUB_DEVICE_CONN_STRING = " + s_connectionString);

            // Connect to the IoT hub using the MQTT protocol
            s_deviceClient = DeviceClient.CreateFromConnectionString(s_connectionString, TransportType.Mqtt);
            await StartSendDeviceToCloudMessageAsync();
            System.Diagnostics.Debug.WriteLine("===== Finished StartMessageSending =====");
        }
    }
        
 }
