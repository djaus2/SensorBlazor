# Blazor Simulated Azure IoT Hub Device with Sensors
A Blazor WASM App simulating sensors on the client, with the server sending the data to an IoT Hub..

**The other half of this functionality is [djaus2/BlazorD2CMessages](https://github.com/djaus2/BlazorD2CMessages): Monitors messages sent to IoT Hub.**

> A Blazor WebAssembly app (with PWA set).  
Simulates an IoT Device with a Tempedrature, Humidity or Acceleraometer sensor.  
Can choose sensor type and set data in client.  
That is then sent to the Service which then forwards it as an IoT Hub message to an Azure IoT Hub

<h3>Sensor Types:</h3>

- temperature
- pressure
- humidity
- luminosity
- accelerometer
- environment
  - accelerometer and environment sensors have 3 values,
- switch
  - switch is binary.  

## Setup
Enter required info in Service\appsettings.json.txt and rename as appsettings.json  
Make sure **appsettings.json** is included in the Build as Content only.

## Pages:
- **Send Data**
  - Set sensor data and send (one message):
    - Set number to send to 1.
    - Choose sensor type
    - Set value/s
    - Then Press [Send]
  - Multiple Random data:
    - Set number to send > 1 and will auto generate random data, including changing sensor type.
      - Option to disable random selection of Sensor Type
    - Set delay for this auto mode.
    - Then Press [Send]
- **Direct Temperature sample**
  - As per Send Data bur comes with one preconfigured Temperature datum.
  - Auto-sends when navigated to.
- **Direct Hunidity sample**
  - As per Send Data bur comes with one preconfigured Hunidity datum.
  - Auto-sends when navigated to.
- **Direct Accelerometer sample**
  - As per Send Data bur comes with one preconfigured Accelerometer datum.
  - Auto-sends when navigated to.
  
  ## .NET Core app in Repository
  Sends data to this service for onforwarding to IoT Hub. By forwarding to this service, it doesnot need to know Hub Details. In that way, the Blazor Service acts and an IoT Hub Edge device.
