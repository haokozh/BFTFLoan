using BFTFLoan.Models;
using BFTFLoan.Models.Services;
using BFTFLoan.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BFTFLoan.Controllers
{
    public class LoanTrialController : Controller
    {
        private readonly LoanTrialService loanTrialService = new LoanTrialService();

        // GET: LoanTrial/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoanTrial/Create
        [HttpPost]
        public ActionResult Create(LoanTrialCreateVM viewModel)
        {
            try
            {
                Session["results"] = loanTrialService.GetLoanTrialResult(viewModel);
                return RedirectToAction("DisplayResult");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return View(viewModel);
        }

        public ActionResult DisplayResult()
        {
            if (ModelState.IsValid)
            {
                var results = (List<LoanTrialVM>)Session["results"];
                return View(results);
            }

            return View();
        }
    }
}
