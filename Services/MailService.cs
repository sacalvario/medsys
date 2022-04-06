﻿using ECN.Contracts.Services;
using ECN.Models;

using System.Net;
using System.Net.Mail;

namespace ECN.Services
{
    public class MailService : IMailService
    {
        public void SendApprovedECN(int id, string generatorname)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            msg.From = new MailAddress("ecnsystem@outlook.com");
            msg.To.Add("controldedocumentos@electri-cord.com.mx");

            msg.Subject = "ECN Aprobado!";
            msg.Body = "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'>Hola<strong><span style='color:black'> Control de documentos!</span></strong></span></span></p>" +
              "<p> &nbsp;</p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'> El<span style='color:#ff0000'><strong> ECN </strong></span> con folio <span style='color:#ff0000'><strong> " + id + " </strong></span>generado por <span style= 'color:#0066cc'><strong>" + generatorname + " </strong></span> ha sido <span style ='color:#339933'><strong> aprobado </strong></span> y se encuentra pendiente de cerrar.</span></span></p> ";

            msg.IsBodyHtml = true;

            client.Port = 587;
            client.Credentials = new NetworkCredential("ecnsystem@outlook.com", "ecmx-ecn");
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch
            {
                return;
            }
        }

        public void SendCancelECN(string email, int id, string generatorname)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            msg.From = new MailAddress("ecnsystem@outlook.com");
            msg.To.Add(email);

            msg.Subject = "ECN Cancelado!";
            msg.Body = "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'>Hola<strong><span style='color:black'> " + generatorname + "! </span></strong></span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'> El <span style='color:#ff0000'><strong> ECN </strong></span> con folio <span style='color:#ff0000'><strong> " + id + " </strong></span> generado por ti, ha sido <span style='color:#ff0000'><strong> cancelado </strong></span> por control de documentos.</span></span></p>";

            msg.IsBodyHtml = true;

            client.Port = 587;
            client.Credentials = new NetworkCredential("ecnsystem@outlook.com", "ecmx-ecn");
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch
            {
                return;
            }
        }

        public void SendCloseECN(string email, int id, string generatorname)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            msg.From = new MailAddress("ecnsystem@outlook.com");
            msg.To.Add(email);

            msg.Subject = "ECN Cerrado!";
            msg.Body = "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'>Hola<strong><span style='color:black'> " + generatorname + "! </span></strong></span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'> El <span style='color:#ff0000'><strong> ECN </strong></span> con folio <span style='color:#ff0000'><strong> " + id + " </strong></span> generado por ti, ha sido <span style='color:#339933'><strong> cerrado </strong></span> por control de documentos.</span></span></p>";

            msg.IsBodyHtml = true;

            client.Port = 587;
            client.Credentials = new NetworkCredential("ecnsystem@outlook.com", "ecmx-ecn");
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch
            {
                return;
            }
        }

        public void SendEmail(string email, int id, string name)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            msg.From = new MailAddress("ecnsystem@outlook.com");
            msg.To.Add(email);

            msg.Subject = "Nuevo ECN!";
            msg.Body = "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'>Hola<strong><span style='color:black'> " + name + "! </span></strong></span></span></p>" +
              "<p><span style = 'font-family:Verdana,Geneva,sans-serif'><span style = 'font-size:12pt'>&nbsp;</span></span></p>" +
              "<p><span style = 'font-family:Verdana,Geneva,sans-serif'><span style = 'font-size:12pt'> Se ha generado un nuevo<strong><span style = 'color:red'> ECN </span ></strong> con el folio<strong><span style = 'color:red'> " + id + " </span></strong></span></span></p>" +
              "<p><span style = 'font-family:Verdana,Geneva,sans-serif'><span style = 'font-size:12pt'> Generado por<strong><span style = 'color:#0066cc'> " + UserRecord.Employee.Name + "</span></strong><span style = 'color:black'>.</span ></strong></span></span></p> ";

            msg.IsBodyHtml = true;

            client.Port = 587;
            client.Credentials = new NetworkCredential("ecnsystem@outlook.com", "ecmx-ecn");
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch
            {
                return;
            }
        }

        public void SendRefuseECNEmail(string email, int id, string signedname, string generatorname)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            msg.From = new MailAddress("ecnsystem@outlook.com");
            msg.To.Add(email);

            msg.Subject = "ECN firmado o pendiente de firmar rechazado!";
            msg.Body = "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'>Hola<strong><span style='color:black'> " + signedname + "! </span></strong></span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'> &nbsp;</span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'> Un <span style='color:#ff0000'><strong> ECN </strong></span> en el que firmaste o estabas proximo a firmar, ha sido rechazado.</span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'> Folio: <strong><span style='color:#ff0000'> " + id + " </span></strong></span></span></p>" +
               "<p><span style='font-size:16px'><span style='font-family:Verdana,Geneva,sans-serif'> Generado por<strong><span style='color:#2980b9'> " + generatorname + "<span></strong>.</span></span></p>";

            msg.IsBodyHtml = true;

            client.Port = 587;
            client.Credentials = new NetworkCredential("ecnsystem@outlook.com", "ecmx-ecn");
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch
            {
                return;
            }
        }

        public void SendRefuseECNToGeneratorEmail(string email, int id, string refusedname, string generatorname)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            msg.From = new MailAddress("ecnsystem@outlook.com");
            msg.To.Add(email);

            msg.Subject = "ECN rechazado!";
            msg.Body = "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'>Hola<strong><span style='color:black'> " + generatorname + "! </span></strong></span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'> &nbsp;</span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'> Un <span style='color:#ff0000'><strong> ECN </strong></span> generado por ti, ha sido rechazado.</span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'> Folio: <strong><span style='color:#ff0000'> " + id + " </span></strong></span></span></p>" +
              "<p><span style='font-size:16px'><span style='font-family:Verdana,Geneva,sans-serif'> Rechazado por<strong><span style='color:#2980b9'> " + refusedname + " </span></strong>.</span></span></p>";

            msg.IsBodyHtml = true;

            client.Port = 587;
            client.Credentials = new NetworkCredential("ecnsystem@outlook.com", "ecmx-ecn");
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch
            {
                return;
            }
        }

        public void SendSignEmail(string email, int id, string signedname, string generatorname)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            msg.From = new MailAddress("ecnsystem@outlook.com");
            msg.To.Add(email);

            msg.Subject = "ECN pendiente de firmar!";
            msg.Body = "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'>Hola<strong><span style='color:black'> " + signedname + "!</span></strong></span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'> &nbsp;</span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'> Tienes un <span style = 'color:#ff0000'><strong> ECN </strong></span> pendiente de firmar</span></span>&nbsp;<span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'>con el folio<span style= 'color:#ff0000'><strong> " + id + "&nbsp;</strong></span></span></span></p>" +
              "<p><span style='font-size:16px'><span style='font-family:Verdana,Geneva,sans-serif'> Generado por<strong><span style='color:#0066cc'> " + generatorname + "</span></strong>.</span></span></p>";

            msg.IsBodyHtml = true;

            client.Port = 587;
            client.Credentials = new NetworkCredential("ecnsystem@outlook.com", "ecmx-ecn");
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch
            {
                return;
            }
        }
    }
}