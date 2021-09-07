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
    public class LoanController : Controller
    {
        private readonly LoanService loanService = new LoanService();

        [Authorize]
        public ActionResult MyLoan()
        {
            // 顯示已登入的 Member 所有的 Loan
            Member member = loanService.FindMemberByAccount(User.Identity.Name);
            List<Loan> loans = loanService.FindAllLoanByBorrowerId(member.Id);
            return View(loans.EntitiesToViewModels());
        }


    }
}