using System;
using System.Configuration;
using System.Net.Mail;

namespace WRT.Core.Util
{
    public static class MailHelper
    {
        //* Check if the SMTP Server is listening on port 25 (telnet ipaddress 25)
        //* Check if your Mailserver ip is being blacklisted (http://mxtoolbox.com/blacklists.aspx)
        //* Check if your SMTP Server is being configured using SMTP Authentication. If using SMTP Authentication, then check if your username and password is correct. You can try login using the username and password using outlook and try to send email from outlook. Remember to check using SMTP Authentication is your outlook settings.
        /// <summary>
        /// Generell funktion för att skicka ett mail (tar inte emot flera mottagar/kopia adresser)
        /// Om inget ska skickas till CC uppge ""
        /// Om inget ska skickas till Bcc uppge ""
        /// Notera att SmtpClient, userName och password hämtas från Web.config
        ///
        /// TO WORK:
        ///   <system.net>
        ///     <mailSettings>
        ///         <smtp from="test@foo.com">
        ///             <network host="smtpserver1" port="25" userName="username" password="secret" defaultCredentials="true" />
        ///         </smtp>
        ///     </mailSettings>
        ///   </system.net>
        /// <add key="SmtpHost" value="mail.mailemall.se"/>
        /// <add key="SmtpMail" value="noreplay@mailemall.se"/>
        /// <add key="SmtpPassword" value="noreplay"/>
        /// 
        ///         ///
        ///Kolla säkerheten på mailservern: http://www.checkor.com/
        /// </summary>
        public static bool SendMail(string message, string receiverAddress, string receiverName, string senderAddress, string senderName, string subject, bool IsBodyHtml)
        {
            string errorCode = string.Empty;
            return SendMail(message, receiverAddress, receiverName, senderAddress, senderName, subject, "", "", "", "", null, IsBodyHtml, false, false, ConfigurationManager.AppSettings["Mail.SmtpHost"], ConfigurationManager.AppSettings["Mail.SenderMail"], ConfigurationManager.AppSettings["Mail.SenderPassword"], ref errorCode, MailPriority.Normal);
        }
        public static bool SendMailThroughGmail(string message, string receiverAddress, string receiverName, string senderAddress, string senderName, string subject, bool IsBodyHtml)
        {
            string errorCode = string.Empty;
            return SendMail(message, receiverAddress, receiverName, senderAddress, senderName, subject, "", "", "", "", null, IsBodyHtml, false, true, "", "", "", ref errorCode, MailPriority.Normal);
        }
        /// <summary>
        /// Undvik att klassas som spam
        /// 
        /// </summary>
        public static bool SendMail(string message, string receiverAddress, string receiverName, string senderAddress, string senderName, string subject, string ccAddress, string CCName, string BccAddress, string BccName, Attachment attachment, bool IsBodyHtml, bool EnableSSL, bool UseGmail, string SmtpHost, string SmtpMail, string SmtpPassword, ref string errorCode, MailPriority prio)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();

                //Receiver
                if (receiverAddress != "")
                    mailMessage.To.Add(new MailAddress(receiverAddress, receiverName));

                //CC
                if (ccAddress != "")
                    mailMessage.CC.Add(new MailAddress(ccAddress, CCName));

                //BCC
                if (BccAddress != "")
                    mailMessage.Bcc.Add(new MailAddress(BccAddress, BccName));

                //Sender
                if (senderAddress != "")
                    mailMessage.From = new MailAddress(senderAddress, senderName);

                //Attachment
                if (attachment != null)
                    mailMessage.Attachments.Add(attachment);

                mailMessage.IsBodyHtml = IsBodyHtml;
                mailMessage.Priority = prio;

                mailMessage.Subject = subject;
                mailMessage.Body = message;

                //Tillägg för att minska risken för SPAM
                mailMessage.Body = PrepareMessage(message, IsBodyHtml);
                mailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(System.Text.RegularExpressions.Regex.Replace(message, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                mailMessage.AlternateViews.Add(plainView);
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(message, null, "text/html");
                mailMessage.AlternateViews.Add(htmlView);

                //Smtpinställningar
                SmtpClient client = new SmtpClient();
                if (!UseGmail)
                {
                    client.Host = SmtpHost;
                    if (EnableSSL)
                    {
                        //http://www.systemnetmail.com/faq/4.5.aspx
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential(SmtpMail, SmtpPassword);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Port = 25;
                        //client.Port = 587;
                    }
                    else
                    {
                        client.EnableSsl = false;
                        client.Port = 25;
                        client.Credentials = new System.Net.NetworkCredential(SmtpMail, SmtpPassword);
                    }
                }
                else
                {
                    //Via Gmail (http://www.andreas-kraus.net/blog/aspnet-20-aka-systemnetmail-with-gmail/)
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.UseDefaultCredentials = false;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                }

                client.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                errorCode = ex.ToString();
                string mess = string.Empty;

                if (errorCode.Contains("The specified string is not in the form required for an e-mail address"))
                    mess = "Adressen är ingen giltig epostadress (" + receiverAddress + ").";
                else if (errorCode.Contains("5.1.1"))//Recipient address rejected
                    mess = "Mottagarens adress avvisades av mailservern (" + receiverAddress + ").";
                else if (errorCode.Contains("4.1.2"))//Domain not found
                    mess = "Mottagarens mailserver kunde inte hittas (" + receiverAddress + ").";
                else if (errorCode.Contains("Unable to connect to the remote server"))
                    mess = "Kunde inte kontakta mottagarens mailserver (" + receiverAddress + "). " + errorCode;
                else
                    mess = "UndefinedError: (" + receiverAddress + ") " + errorCode;

                SendMail(mess, "richard.segerlund@gmail.com", "Richard", "noreplay@mailemall.se", "Error", "Error sending mail", false);
                return false;
            }
        }
        public static string AddTopBottom(string message, bool addBorder, string borderColor)
        {
            string back = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>";
            back += "<html xmlns='http://www.w3.org/1999/xhtml'>";
            back += "<head>";
            back += "<title>Mail</title>";
            back += "</head>";
            back += "<body>";
            if (addBorder)
                back += "<br /><div style='font-family: Verdana; font-size: 10px;border: solid 3px " + borderColor + ";background-color:White;padding:10px;width:600px;margin-left:10px;'>";
            back += message;
            if (addBorder)
                back += "</div><br /><br /><br />";
            back += "</body>";
            back += "</html>";

            return back;
        }
        private static string PrepareMessage(string message, bool isBodyHtml)
        {
            if (isBodyHtml)
            {
                bool docType = message.Contains("<!DOCTYPE") ? true : false;
                bool html = message.Contains("<html") ? true : false;
                bool head = message.Contains("<head") ? true : false;
                bool body = message.Contains("<body") ? true : false;

                //    const string doctyp = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'>";
                //    if (!message.Contains("<body>"))
                //        message = "<body>" + message + "</body>";
                //    if (!message.Contains("<!DOCTYPE"))
                //        message = doctyp + message + "</html>";
            }
            else
            { }

            return message;
        }
    }
}