using BFTFLoan.Models.EFModels;
using BFTFLoan.Models.Services;
using BFTFLoan.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BFTFLoan.Controllers
{
    public class InvestmentController : Controller
    {
        private readonly InvestmentService investmentService = new InvestmentService();
        public ActionResult MyInvestment()
        {
            Member member = investmentService.FindMemberByAccount(User.Identity.Name);
            List<Investment> investments = investmentService.FindAllInvestmentByInvestorId(member.Id);
            return View(investments.EntitiesToViewModels());
        }
    }
}