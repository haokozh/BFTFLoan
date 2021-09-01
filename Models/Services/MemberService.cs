using BFTFLoan.Models.EFModels;
using BFTFLoan.Models.Repositories;
using BFTFLoan.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BFTFLoan.Models.Services
{
    public class MemberService
    {
        private readonly MemberRepository repository = new MemberRepository();

        public void Register(Member member)
        {
            if (repository.IsEmailExists(member.Email))
            {
                throw new Exception("此信箱已被註冊");
            }

            repository.Create(member);
        }

        public void Login(LoginVM viewModel)
        {

        }

        public void ForgetPassword()
        {

        }

        // throw Exception
        public void VerifyEmail(string email)
        {
            if (repository.IsEmailExists(email))
            {
                SendEmail(email);
            }

            // throw Exception
        }

        public void SendEmail(string email)
        {
            MailAddress mailTo = new MailAddress(email);
            MailAddress mailFrom = new MailAddress("peter9617189@gmail.com");
            MailMessage mailMessage = new MailMessage(mailFrom, mailTo);

            mailMessage.Subject = "Email Verification";
            mailMessage.Body = $"Your verification code: {GetOneTimePassword()}";

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;

            smtpClient.Credentials = new NetworkCredential("peter9617189@gmail.com", "zalbvkerbkrovtou");

            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }

        public string GetOneTimePassword()
        {
            string OTPResult = "";
            int OTPLength = 6;
            string[] allowedChars = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

            Random random = new Random();
            for (int i = 0; i < OTPLength; i++)
            {
                OTPResult += allowedChars[random.Next(0, allowedChars.Length)];
            }

            return OTPResult;
        }
    }
}