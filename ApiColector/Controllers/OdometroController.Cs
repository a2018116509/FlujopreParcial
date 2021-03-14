namespace ApiColector.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Azure.Messaging.ServiceBus;
    using ApiColector.Models;
    using Newtonsoft.Json;
    [Route("api/[controller]")]
    [ApiController]
    public class OdometroController : ControllerBase
    {
        [HttpPost]
        public async Task<bool> EnviarAsync([FromBody] Odometro odometro ) 
        {
            string connectionString = "Endpoint=sb://queueejerciciosma.servicebus.windows.net/;SharedAccessKeyName=enviar;SharedAccessKey=5vTV8Sz7ZYKZwb/Lrm9+YWlCTM9vO3kkL3oILOLhwaA=;EntityPath=ejercicios";
            string queueName = "ejercicios";
            string mensaje = JsonConvert.SerializeObject(odometro);

            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(queueName);

                // create a message that we can send
                ServiceBusMessage message = new ServiceBusMessage(mensaje);

                // send the message
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Sent a single message to the queue: {queueName}");
            }
            return true;
        } 
    }
}
