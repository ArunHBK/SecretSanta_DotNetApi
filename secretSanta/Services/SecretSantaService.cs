using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using secretSanta.Models;
using System;
using MailKit.Net.Smtp;

using System.Net;

namespace secretSanta.Services
{
    public class SecretSantaService : ISecretSantaService

    {
        private readonly IConfiguration _config;
        public SecretSantaService(IConfiguration config)
        {
            _config = config;
        }
        ResponseObject response = new ResponseObject();

        public async Task<ResponseObject> Randomizer(List<playerDetails> playerList)
        {
            try 
            { 
            if (playerList.Count !=0)
            {
                List<string> senderEmailList = new List<string>();
                List<string> emailTempList = new List<string>();
                foreach (var item in playerList)
                {
                    emailTempList.Clear();
                    foreach (var item2 in playerList)
                    {
                        bool flag = senderEmailList.Contains(item2.email);
                        if (item.name == item2.name || flag)
                            continue;
                        emailTempList.Add(item2.email);

                    }
                    Random r = new Random();
                    int rInt = r.Next(emailTempList.Count);
                    senderEmailList.Add(emailTempList[rInt]);

                }
                response.Status = true;
                response.StatusCode= StatusCodes.Status200OK;
                response.Message="Mail Sent Successfull";
                response.Value = senderEmailList;
                return response;
            }
            else
            {
                response.Status = false;
                response.StatusCode= StatusCodes.Status400BadRequest;
                response.Message="Username and Email should be valid";
                return response;
            }
        }
             catch (Exception ex)
            {
                response.Status=false;
                response.StatusCode= StatusCodes.Status500InternalServerError;
                response.Message=ex.Message;
                return response;
            }
        }

       

       public void sendEmailToList(List<playerDetails> playerList,List<string> senderEmailList)
           {
            
            for(int i=0;i<playerList.Count;i++)
            {
                SendEmail($"For this christmas buy a gift for {playerList[i].name}.", senderEmailList[i]);
            }
        }

        public void SendEmail(string Body,string To)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(To));
            email.Subject = "Your secret santa buddy is...";
            email.Body = new TextPart(TextFormat.Html) { Text = Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

        }


    }
}