using BFTFLoan.Models.EFModels;
using BFTFLoan.Models.Repositories;
using BFTFLoan.Models.ViewModels;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Security;

namespace BFTFLoan.Models.Services
{
    public class MemberService
    {
        private readonly MemberRepository memberRepository = new MemberRepository();

        // complete
        #region 註冊
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
            string hashedPassword = Hash(password);
            Member member = viewModel.ViewModelToEntity(hashedPassword, IsEmailVerified);

            memberRepository.Create(member);
            #endregion
        }
        #endregion

        // complete
        #region 登入
        // 登入驗證
        public HttpCookie GetEncryptedCookie(LoginVM viewModel)
        {
            if (IsMemberExists(viewModel))
            {
                bool isRemeberMe = viewModel.RemeberMe;
                string account = viewModel.Account;

                // 設定登入持續時間
                // 525600 分鐘 => 1 年
                int timeout = isRemeberMe ? 525600 : 20;

                // 新增表單驗證 ticket
                var ticket = new FormsAuthenticationTicket(account, isRemeberMe, timeout);

                // 加密 ticket
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                // 建立 cookie
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                {
                    Expires = DateTime.Now.AddMinutes(timeout),
                    HttpOnly = true
                };

                return cookie;
            } 
            
            throw new Exception("帳號或密碼錯誤");
        }

        private bool IsMemberExists(LoginVM viewModel)
        {
            string account = viewModel.Account;
            string hashedPassword = Hash(viewModel.Password);

            Member member = memberRepository
                .FindMemberByAccountAndPassword(account, hashedPassword);

            return member != null;
        }
        #endregion

        // complete
        #region 登出
        // 登出
        public void Logout()
        {

        }
        #endregion

        // complete
        #region 重設密碼
        public void ResetPassword(Member member, ResetPasswordVM viewModel)
        {

            string newPassword = viewModel.NewPassword;
            string newConfirmPassword = viewModel.ConfirmPassword;

            if (!IsPasswordEqualsToConfirmPassword(newPassword, newConfirmPassword))
            {
                throw new Exception("新密碼與確認密碼不相符");
            }

            member.Password = Hash(newPassword);
            memberRepository.UpdatePassword(member);
        }
        #endregion

        // complete
        #region 依照 Email 尋找某一筆 Member 資料
        public Member FindMemberByEmail(string email)
        {
            return memberRepository.FindMemberByEmail(email);
        }
        #endregion

        // complete
        #region 更新 IsEmailVerified
        public void UpdateIsEmailVerified(Member member, bool isEmailVerified)
        {
            member.IsEmailVerified = isEmailVerified;
            memberRepository.UpdateIsEmailVerified(member);
        }
        #endregion

        // complete
        #region 發送驗證信 & 驗證 OTP
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
        #endregion

        // complete
        #region 產生 OTP
        // 產生 OTP
        public Totp GetOTP()
        {
            var secret = Base32Encoding.ToBytes("6L4OH6DDC4PLNQBA5422GM67KXRDIQQP");
            var totp = new Totp(secret, step: 300);

            return totp;
        }
        #endregion

        // complete
        #region 帳號是否存在
        // 帳號是否存在
        private bool IsAccountExists(string account)
        {
            return memberRepository.IsAccountExists(account);
        }
        #endregion

        // complete
        #region 信箱是否存在
        // 信箱是否存在
        private bool IsEmailExists(string email)
        {
            return memberRepository.IsEmailExists(email);
        }
        #endregion

        // complete
        #region 對密碼做 Hash 運算
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
        #endregion
    }
}