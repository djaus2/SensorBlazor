# BlazorSensors
A Blazor WASM App simuoating sensors on the client, with the server sending the data to an IoT Hub.

> A Blazor WebAssembly app (with PWA set).  
Simulates an IoT Device with a Tempedrature, Humidity or Acceleraometer sensor.  
Can choose sensor type and set data in client.  
That is then sent to the Service which then forwards it as an IoT Hub message to an Azure IoT Hub.  

## Pages:
- **Start Service**
  - Start service on Server to monitor http Posts from Client.
  - Client will post IOT Hub Sensor data as Json string.
  - When received, form message and send to IoT Hub
- **Stop Service**
  - Stop that service on Server.
- **Send Data**
  - Set sensor data ready to send
    - Choose sensor type
    - Set value/s
  - Post data
- **Direct Temperature sample**
  - As per Send Data bur comes with preconfigured Temperature data.
  - Auto-sends when navigated to.
- **Direct Hunidity sample**
  - As per Send Data bur comes with preconfigured Hunidity data.
  - Auto-sends when navigated to.
- **Direct Accelerometer sample**
  - As per Send Data bur comes with preconfigured Accelerometer data.
  - Auto-sends when navigated to.
