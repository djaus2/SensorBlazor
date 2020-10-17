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
  - Set sensor data ready to send
    - Set number to send to 1.
    - Choose sensor type
    - Set value/s
  - Post data
    - Set Number To Send to 1 to send set data.
    - Or set number to send > 1 and will auto generate random data, including changing sensor type.
      - Option to disable random selection of Sensor Type
    - Set delay for this auto mode.
    - Then Press [Send]
- **Direct Temperature sample**
  - As per Send Data bur comes with preconfigured Temperature data.
  - Auto-sends when navigated to.
- **Direct Hunidity sample**
  - As per Send Data bur comes with preconfigured Hunidity data.
  - Auto-sends when navigated to.
- **Direct Accelerometer sample**
  - As per Send Data bur comes with preconfigured Accelerometer data.
  - Auto-sends when navigated to.
