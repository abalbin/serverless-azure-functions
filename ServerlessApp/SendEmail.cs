using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Blob;
using SendGrid.Helpers.Mail;
using ServerlessApp.Models;

namespace ServerlessApp
{
    public static class SendEmail
    {
        [FunctionName("SendEmail")]
        public static void Run([BlobTrigger("licences/{name}.lic", Connection = "")]CloudBlockBlob licenceBlob, string name, 
            [Table("orders", "stripe", "{name}")] Order order,
            [SendGrid] out SendGridMessage message,
            TraceWriter log)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{name}");
            message = new SendGridMessage();
            message.AddTo("chsakell@gmail.com");
            message.AddContent("text/html", $"Download your licence <a href='{licenceBlob.Uri.AbsoluteUri}' alt='Licence link'>here</a>");
            message.SetFrom(new EmailAddress("myemailaddress@example.com"));
            message.SetSubject("Your payment has been completed");
        }
    }
}
