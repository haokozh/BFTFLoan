using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.ViewModels
{
    public class LoanTrialCreateViewModel
    {
        // 本金
        [Display(Name = "本金")]
        public decimal Principal { get; set; }


        // 年利率
        private double _annualRate;
        [Display(Name = "年利率")]
        public double AnnualRate
        {
            get => _annualRate;
            set => _annualRate = value / 100;
        }

        // 期數
        [Display(Name = "期數")]
        public int NumOfPeriods { get; set; }
    }
}