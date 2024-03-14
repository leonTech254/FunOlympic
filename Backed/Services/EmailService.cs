using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Backed.Services
{
    public class EmailService
    {
        internal void SendWelcomeEmail()
        {

        }
        private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
        internal async Task SendWelcomeMessage(User userDetails)
        {
            string subject = "Welcome to AuctionHouse";
            string body = $@"<!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <title>{subject}</title>
                        <style>
                            body {{
                                font-family: 'Arial', sans-serif;
                                background-color: #f4f4f4;
                                color: #333;
                                margin: 0;
                                padding: 0;
                            }}
                            .card {{
                                max-width: 600px;
                                margin: 20px auto;
                                background-color: #fff;
                                border: 1px solid #ddd;
                                border-radius: 8px;
                                overflow: hidden;
                            }}
                            .container {{
                                padding: 20px;
                            }}
                            h1 {{
                                color: #007bff;
                            }}
                            p {{
                                line-height: 1.6;
                            }}
                            .verification-code {{
                                color: #007bff;
                               
                                padding: 10px;
                                border-radius: 5px;
                                margin-top: 20px;
								text-transform: uppercase;
								font-weight: bold;
								font-size: 18px;
                            }}
                            .contact-message {{
                                color: #888;
                                margin-top: 20px;
                            }}
							.verConde{{
								color: green;
								font-size: 22px;
							}}
                        </style>
                    </head>
                    <body>
                        <div class=""card"">
                            <div class=""container"">
                               <h1>{subject}</h1>
        					   <p>Hello, <strong>{userDetails.FirstName}</strong>,</p>
        						<p>You have successfully created your account.</p>
                                <p class=""verification-code"">You can now log in to our system using the username and password you registered with.</p>
                                <p class=""contact-message"">If you received this message by mistake, please contact martinleontech23@gmail.com.</p>
                            </div>
                        </div>
                    </body>
                    </html>";
                await SendEmail(userDetails, subject, body);

        }
       public async Task SendEmail(User userDetails, String subject,String body)
		{
			String useremail = userDetails.Email;
			String username = userDetails.FirstName;
			Console.WriteLine($"{useremail} {username}");
			try
			{
				var message = new MimeMessage();
				message.From.Add(new MailboxAddress("AuctionHouse", "leonteqsec@gmail.com"));
				message.To.Add(new MailboxAddress(username, useremail));
				message.Subject = subject;
				// message.Body = new TextPart("plain")
				message.Body = new TextPart("html")
				{
					Text =body
				};

				var smtpHost = _configuration["SmtpConfig:Host"];
				var smtpPort = int.Parse(_configuration["SmtpConfig:Port"]);
				var smtpUser = _configuration["SmtpConfig:Username"];
				var smtpPassword = _configuration["SmtpConfig:Password"];

				using (var client = new SmtpClient())
				{
					client.Connect(smtpHost, smtpPort, false);
					client.Authenticate(smtpUser, smtpPassword);

					await client.SendAsync(message);
					client.Disconnect(true);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

		}

        
    }
}