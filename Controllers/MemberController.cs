using BFTFLoan.Models.EFModels;
using BFTFLoan.Models.Services;
using BFTFLoan.Models.ViewModels;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BFTFLoan.Controllers
{
    public class MemberController : Controller
    {
        private readonly MemberService memberService = new MemberService();

        // complete
        #region 註冊
        // Register GET
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // Register POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(RegisterVM viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    memberService.Register(viewModel);
                    Session["userEmail"] = viewModel.Email;
                    return RedirectToAction("VerifyEmail");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return View(viewModel);
        }
        #endregion

        #region 登入
        //Login GET
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginVM viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // 取得加密後的 Cookie
                    var cookie = memberService.GetEncryptedCookie(viewModel);

                    // 送回 client 端
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return View(viewModel);
        }
        #endregion

        #region 登出
        // Logout GET
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        // todo
        #region 忘記密碼
        // Forget Password GET
        [AllowAnonymous]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        // Forget Password POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult ForgetPassword(ForgetPasswordVM viewModel)
        {
            return View();
        }
        #endregion

        // complete
        #region 驗證 Email
        // Verify Email GET
        public ActionResult VerifyEmail()
        {
            try
            {
                // 使用者的註冊信箱
                string userEmail = Convert.ToString(Session["userEmail"]);

                // 取得 OTP
                Totp totp = memberService.GetOTP();

                // 將 Base32 的 byte array 轉成 6 位數的 OTP
                string result = totp.ComputeTotp();

                // 發送驗證信
                memberService.SendEmail(userEmail, result);

                Session["totp"] = totp;
                ViewBag.RemainingSeconds = totp.RemainingSeconds();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return View();
        }

        // verify email post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyEmail(VerifyEmailVM viewModel)
        {
            try
            {
                Totp totp = (Totp)Session["totp"];

                // 驗證輸入的字串是否和產生的 OTP 相同
                bool isVerify = memberService.VerifyOTP(totp, viewModel.OTPUserInput);

                Session["verifyResult"] = isVerify;

                if (isVerify)
                {
                    // 使用者的註冊信箱
                    string userEmail = Convert.ToString(Session["userEmail"]);
                    Member member = memberService.FindMemberByEmail(userEmail);
                    memberService.UpdateIsEmailVerified(member);
                }

                return RedirectToAction("Confirm");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return View(viewModel);
        }

        public ActionResult Confirm()
        {
            bool verifyResult = Convert.ToBoolean(Session["verifyResult"]);

            ViewBag.VerifyResult = verifyResult ? "Successful" : "Failed";

            return View();
        }
        #endregion
    }
}