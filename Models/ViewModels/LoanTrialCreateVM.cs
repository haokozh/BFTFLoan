using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.ViewModels
{
    public class LoanTrialCreateVM
    {
        // 本金
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "本金")]
        public decimal Principal { get; set; }


        // 年利率
        private double _annualRate;

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "年利率")]
        public double AnnualRate
        {
            get => _annualRate;
            set
            {
                if (value < 1)
                {
                    _annualRate = value;
                }
                else
                {
                    _annualRate = value / 100;
                }
            }
        }

        // 期數
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "期數")]
        public int NumOfPeriods { get; set; }
    }
}