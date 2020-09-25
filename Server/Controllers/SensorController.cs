using BlazorSensorApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;
using Newtonsoft.Json;

namespace BlazorSensorApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorController : ControllerBase
    {

        private readonly ILogger<SensorController> logger;

        public SensorController(ILogger<SensorController> logger)
        {
            this.logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> Post(object obj)
        {
            bool state;
            Sensor sensor;

            string json = obj.ToString();

            if (bool.TryParse(json, out state))
            {
                await Task.Delay(333);
                if (state)
                    Task.Run(() => SimulatedDevice.StartMessageSending()).GetAwaiter();
                else
                    SimulatedDevice.StopMessageSending();
                return Ok(SimulatedDevice.KeepRunning);
            }
            else 
            {
                try
                {
                    sensor = JsonConvert.DeserializeObject<Sensor>(json);
                    if (sensor != null)
                    {
                        if (!SimulatedDevice.KeepRunning)
                            await SimulatedDevice.StartMessageSending();
                        SimulatedDevice.SendMessage(sensor);
                        //await Task.Delay(1000);
                        return Ok(sensor.Id);
                    }
                    else
                        return BadRequest();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
        }

    }
}

