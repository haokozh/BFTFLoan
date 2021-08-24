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

        // GET: LoanTrial
        public ActionResult Index()
        { 
            return View();
        }

        // GET: LoanTrial/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoanTrial/Create
        [HttpPost]
        public ActionResult Create(LoanTrialCreateViewModel viewModel)
        {
            try
            {
                Session["results"] = loanTrialService.GetLoanTrialResult(viewModel);
                return RedirectToAction("DisplayResult");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DisplayResult()
        {
            var results = (List<LoanTrialViewModel>)Session["results"];
            return View(results);
        }
    }
}
