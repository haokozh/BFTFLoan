using BFTFLoan.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.ViewModels
{
    public class LoanVM
    {
        [Display(Name = "本金")]
        public decimal Principal { get; set; }

        [Display(Name = "年利率")]
        public double InterestRate { get; set; }

        [Display(Name = "期數")]
        public int NumOfPeriods { get; set; }

        [Display(Name = "申貸原因")]
        public string Reason { get; set; }

        [Display(Name = "申請日期")]
        public DateTime CreationTime { get; set; }
    }
}