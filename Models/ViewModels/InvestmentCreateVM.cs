using BFTFLoan.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.ViewModels
{
    public class InvestmentCreateVM
    {
        [Display(Name = "投資案件編號")]
        public int LoanId { get; set; }

        [Display(Name = "投資金額")]
        public decimal Amount { get; set; }
        
        [Display(Name = "年化報酬率")]
        public double IRR { get; set; }
    }

    public static class InvestmentAssembler
    {
        public static Investment ViewModelToEntity(this InvestmentCreateVM viewModel, int investorId)
        {
            return new Investment
            {
                InvestorId = investorId,
                LoanId = viewModel.LoanId,
                Amount = viewModel.Amount,
                IRR = viewModel.IRR,
                CreationTime = DateTime.Now
            };
        }

        public static InvestmentVM EntityToViewModel(this Investment investment)
        {
            return new InvestmentVM
            {
                LoanId = investment.LoanId,
                Amount = investment.Amount,
                IRR = investment.IRR,
                CreationTime = investment.CreationTime
            };
        }

        public static List<InvestmentVM> EntitiesToViewModels(this List<Investment> investments)
        {
            return investments.Select(i => i.EntityToViewModel()).ToList();
        }
    }
}