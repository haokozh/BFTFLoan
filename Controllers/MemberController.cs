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

namespace BFTFLoan.Controllers
{
    public class MemberController : Controller
    {
        private readonly MemberService memberService = new MemberService();

        // Register GET
        public ActionResult Register()
        {
            return View();
        }

        // Register POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM viewModel)
        {
            try
            {
                memberService.Register(viewModel);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            if (ModelState.IsValid)
            {
                Session["userEmail"] = viewModel.Email;
                return RedirectToAction("VerifyEmail");
            }

            return View(viewModel);
        }

        //Login GET
        public ActionResult Login()
        {
            return View();
        }

        // Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string account, string password)
        {
            return View();
        }

        // Verify Email GET
        public ActionResult VerifyEmail()
        {
            try
            {
                // 使用者的註冊信箱
                string userEmail = (string)Session["userEmail"];

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
                bool verify = memberService.VerifyOTP(totp, viewModel.OTPUserInput);

                Session["verifyResult"] = verify;

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
            ViewBag.VerifyResult = Convert.ToBoolean(Session["verifyResult"]);

            return View();
        }
    }
}