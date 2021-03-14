namespace fncConsumidor
{
    using System;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using System.Net.Mail;
    using Newtonsoft.Json;
    using fncConsumidor.Models;
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run(
            [ServiceBusTrigger(
                    "ejercicios",
                    Connection = "MyConn"
            )] string myQueueItem,

            ILogger log
        )
        {
            try
            {
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
                var dataOdometer = JsonConvert.DeserializeObject<Odometro>(myQueueItem);
                //Aqui va la accion await datos.AddAsync(data);
                MailMessage message = new MailMessage();
                 message.To.Add(dataOdometer.Email); //Email from queue
                message.Subject = "YourOdometer";
                message.From = new MailAddress("mariana.arnez@outlook.es"); //My Email
                message.Body = $"Hola! {dataOdometer.Name}, desde la última toma, caminaste {dataOdometer.Step} pasos.";

                SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com");
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("mariana.arnez@outlook.es", "abril,2003");
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                log.LogError($"It is not possible to send mails: {ex.Message}");
            }
        }


    }
}
