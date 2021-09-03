using BFTFLoan.Models.EFModels;
using BFTFLoan.Models.ViewModels;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace BFTFLoan.Models.Services
{
    public class MemberService
    {
        private AppDbContext db = new AppDbContext();

        // 註冊
        public void Register(RegisterVM viewModel)
        {
            #region 檢查帳號是否存在
            if (IsAccountExists(viewModel.Account))
            {
                throw new Exception("此帳號已被註冊");
            }
            #endregion

            #region 檢查信箱是否存在
            if (IsEmailExists(viewModel.Email))
            {
                throw new Exception("此信箱已被註冊");
            }
            #endregion

            #region 檢查密碼與確認密碼是否相同
            string password = viewModel.Password;
            string confirmPassword = viewModel.ConfirmPassword;

            if (!IsPasswordEqualsToConfirmPassword(password, confirmPassword))
            {
                throw new Exception("密碼與確認密碼不相符");
            }
            #endregion

            #region INSERT 註冊資料
            bool IsEmailVerified = false;
            Member member = viewModel.ViewModelToEntity(IsEmailVerified);

            db.Member.Add(member);
            db.SaveChanges();
            #endregion
        }
        // 登入
        public void Login()
        {

        }

        // 登出
        public void Logout()
        {

        }

        // 驗證 OTP
        public bool VerifyOTP(Totp totp, string userInput)
        {
            return totp.VerifyTotp(userInput, out long timeStepMatched, window: null);
        }

        // 發送驗證信
        public void SendEmail(string to, string otp)
        {
            string from = "peter9617189@gmail.com";

            // 收件人郵件地址
            MailAddress mailTo = new MailAddress(to);

            // 寄件人郵件地址
            MailAddress mailFrom = new MailAddress(from);

            // 郵件訊息
            MailMessage mailMessage = new MailMessage(mailFrom, mailTo);

            // 主旨
            mailMessage.Subject = "Your verification code is ...";

            // 內容
            mailMessage.Body = $"Verification Code: {otp}";

            // 使用 SMTP 協定傳送郵件
            SmtpClient smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,

                Credentials = new NetworkCredential(from, "aftcroespbhyeyqf"),
                EnableSsl = true
            };

            smtpClient.Send(mailMessage);
        }

        // 產生 OTP
        public Totp GetOTP()
        {
            var secret = Base32Encoding.ToBytes("6L4OH6DDC4PLNQBA5422GM67KXRDIQQP");
            var totp = new Totp(secret, step: 300);

            return totp;
        }

        // 帳號是否存在
        private bool IsAccountExists(string account)
        {
            return db.Member
                .Where(m => m.Account == account)
                .FirstOrDefault() != null;
        }

        // 信箱是否存在
        private bool IsEmailExists(string email)
        {
            return db.Member
                .Where(m => m.Email == email)
                .FirstOrDefault() != null;
        }

        // 檢查密碼與確認密碼是否相等
        private bool IsPasswordEqualsToConfirmPassword(string password, string confirmPassword)
        {
            return Hash(password) == Hash(confirmPassword);
        }

        // 對註冊密碼做 Hash 運算
        private static string Hash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                );
        }
    }
}