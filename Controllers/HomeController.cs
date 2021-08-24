using BFTFLoan.Models;
using BFTFLoan.Models.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BFTFLoan.Controllers
{
    public class HomeController : Controller
    {
        private readonly LoanTrialService loanTrialService = new LoanTrialService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}