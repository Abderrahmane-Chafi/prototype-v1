using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    using MimeKit;
    using MailKit.Net.Smtp;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;

    public class EmailSender : IEmailSender
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmailSender(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Create a new email message
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse("contact@cash-flow-creators.net"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;

            // Create the body part (HTML message)
            var body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            // Create a multipart to hold the message body and potentially the attachment
            var multipart = new Multipart("mixed");
            multipart.Add(body);

            // Check if the recipient email is NOT "contact@ca-web-solutions.net"
            if (email != "contact@cash-flow-creators.net")
            {
                // Get the absolute path of the PDF file
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "leadmagnet", "Guide ultime pour mettre votre entreprise en ligne.pdf");

                // Create the attachment part
                var attachment = new MimePart("application", "pdf")
                {
                    Content = new MimeContent(File.OpenRead(filePath)),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = "Guide ultime pour mettre votre entreprise en ligne.pdf"
                };

                // Add the attachment to the multipart
                multipart.Add(attachment);
            }

            // Set the multipart as the message body
            emailToSend.Body = multipart;

            // Send the email
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("contact@cash-flow-creators.net", "vqrilybcioasstyf");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }

            return Task.CompletedTask;
        }
    }

}
