using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.ViewModels
{
    public class LoanTrialVM
    {
        // 當前期數
        [Display(Name = "當前期數")]
        public int CurrentNumOfPeriods { get; set; }

        // 當期應付款
        [Display(Name = "當期應付款")]
        public decimal CurrentPayable { get; set; }

        // 當期應付利息
        [Display(Name = "當期應付利息")]
        public decimal CurrentInterestPayable { get; set; }

        // 當期應付本金
        [Display(Name = "當期應付本金")]
        public decimal CurrentPrincipalPayable { get; set; }

        // 當期剩餘本金
        [Display(Name = "當期剩餘本金")]
        public decimal RemainPrincipal { get; set; }
    }
}