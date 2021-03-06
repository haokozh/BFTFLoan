using BFTFLoan.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.ViewModels
{
    public class LoanTrialCreateVM
    {
        private decimal _principal;

        // 本金
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "本金")]
        [Range(3000, 100000, ErrorMessage = "{0}必須介於 {1} 元 ~ {2} 元之間")]
        public decimal Principal
        {
            get => _principal;
            set
            {
                if (value < 3000)
                {
                    _principal = 3000;
                }
                else if (value > 100000)
                {
                    _principal = 100000;
                }
                else
                {
                    _principal = value;
                }
            }
        }


        // 年利率
        private double _annualRate;

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "年利率")]
        public double AnnualRate
        {
            get => _annualRate;
            set
            {
                if (value >= 3 && value <= 16)
                {
                    _annualRate = value / 100;
                }
                else if (value >= 0.03 && value <= 0.16)
                {
                    _annualRate = value;
                }
                else
                {
                    _annualRate = 0.03;
                }
            }
        }

        // 期數
        private int _numOfPeriods;

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "期數")]
        [Range(1, 12, ErrorMessage = "{0}必須介於 {1} ~ {2} 之間")]
        public int NumOfPeriods
        {
            get => _numOfPeriods;
            set
            {
                if (value < 1)
                {
                    _numOfPeriods = 1;
                }
                else if (value <= 12)
                {
                    _numOfPeriods = value;
                }
                else
                {
                    _numOfPeriods = 12;
                }
            }
        }
    }
}