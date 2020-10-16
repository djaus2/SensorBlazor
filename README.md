# Blazor Simulated Azure IoT Hub Device with Sensors
A Blazor WASM App simulating sensors on the client, with the server sending the data to an IoT Hub..

**The other half of this functionality is [djaus2/BlazorD2CMessages](https://github.com/djaus2/BlazorD2CMessages): Monitors messages sent to IoT Hub.**

> A Blazor WebAssembly app (with PWA set).  
Simulates an IoT Device with a Tempedrature, Humidity or Acceleraometer sensor.  
Can choose sensor type and set data in client.  
That is then sent to the Service which then forwards it as an IoT Hub message to an Azure IoT Hub.

## Usage
Enter require info in Servre\appsettings.json.txt and rename as appsettings.json
Make sure it is included inteh Build as content

Need to update this

## Pages:
- ~~**Start Service**~~
  - ~~Start service on Server to monitor http Posts from Client.~~
  - ~~Client will Post IoT Hub Sensor data as Json string to Server.~~
  - ~~When received, form message and send to IoT Hub~~
- ~~**Stop Service**~~
  - ~~Stop that service on Server.~~
- **Send Data**
  - Set sensor data ready to send
    - Set number to send to 1.
    - Choose sensor type
    - Set value/s
  - Post data
    - Set Number To Send to 1 to send set data.
    - Or set number to send > 1 and will auto generate random data, including changing sensor type.
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
