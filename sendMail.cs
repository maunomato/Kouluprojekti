static void SendMail()
{
            string _USERNAME = ""; //careerian sahkopostiosoite
            string _PASSWORD = ""; //careerian sp salasana
            string senderEmail = ""; //careerian sp

            using (SmtpClient client = new SmtpClient()
            {
                Host = "smtp.office365.com",
                Port = 587,
                UseDefaultCredentials = false, // This require to be before setting Credentials property
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_USERNAME, _PASSWORD), // you must give a full email address for authentication 
                TargetName = "STARTTLS/smtp.office365.com", // Set to avoid MustIssueStartTlsFirst exception
                EnableSsl = true // Set to avoid secure connection exception
            })
            {

                MailMessage message = new MailMessage()
                {
                    From = new MailAddress(senderEmail), // sender must be a full email address
                    Subject = "Something",
                    IsBodyHtml = true,
                    Body = "<h1>Hello World</h1>",
                    BodyEncoding = System.Text.Encoding.UTF8,
                    SubjectEncoding = System.Text.Encoding.UTF8,

                };
                message.To.Add("vastaanottajansahkoposti@tahanlaitetaan.com");

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
}