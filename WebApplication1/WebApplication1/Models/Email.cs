using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace WebApplication1.Models
{
    public class Email : SmtpClient
    {
        public Email()
        {
            Host = "smtp.gmail.com";
            Port = 587;
            UseDefaultCredentials = false;
            Credentials = new System.Net.NetworkCredential("electronic.gift.card@gmail.com", "123456789ASD");// Enter seders User name and password  
            EnableSsl = true;
        }

    }
}