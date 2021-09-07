using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.ViewModels
{
    public class InvestmentVM
    {
        [Display(Name = "貸款編號")]
        public int LoanId { get; set; }

        [Display(Name = "投資金額")]
        public decimal Amount { get; set; }

        [Display(Name = "年化報酬率")]
        public double IRR { get; set; }

        [Display(Name = "投資日期")]
        public DateTime CreationTime { get; set; }
    }
}