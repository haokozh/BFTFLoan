using BFTFLoan.Models.Services;
using BFTFLoan.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BFTFLoan.Controllers
{
    public class MemberController : Controller
    {
        private readonly MemberService memberService = new MemberService();

        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM viewModel)
        {
            memberService.Login(viewModel);
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM viewModel)
        {
            try
            {
                memberService.Register(viewModel.ViewModelToEntity());
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}