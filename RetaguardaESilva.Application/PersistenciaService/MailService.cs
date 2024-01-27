using RetaguardaESilva.Application.ContratosServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RetaguardaESilva.Persistence.Migrations;

namespace RetaguardaESilva.Application.PersistenciaService
{
    public class MailService : IMailService
    {
        private string smtpAddress => "smtp.office365.com";
        private int portNumber => 587;
        private string emailFromAddress => "retaguardaesilva@hotmail.com";
        private string password => "jesusCRISTO@33";
        public void AddEmailsToMailMessage(MailMessage mailMessage, string email)
        {
            mailMessage.To.Add(email);
        }

        public void SendMail(string email, string subject, string body, bool anexo, int? notaFiscalId, bool? exclusao, bool isHtml = false)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(emailFromAddress);
                AddEmailsToMailMessage(mailMessage, email);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                if (anexo == true && exclusao == false)
                {
                    Attachment anexar = new Attachment($"C:\\Users\\Felipe Silva\\Downloads\\Nota Fiscal {notaFiscalId}.pdf");
                    mailMessage.Attachments.Add(anexar);
                }
                else if (anexo == true && exclusao == true)
                {
                    Attachment anexar = new Attachment($"C:\\Users\\Felipe Silva\\Downloads\\Nota Fiscal {notaFiscalId} cancelada.pdf");
                    mailMessage.Attachments.Add(anexar);
                }
                mailMessage.IsBodyHtml = isHtml;
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.Send(mailMessage);
                }
            }
        }
    }
}
