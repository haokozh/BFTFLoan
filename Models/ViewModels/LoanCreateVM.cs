using BFTFLoan.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BFTFLoan.Models.ViewModels
{
    public class LoanCreateVM
    {
        private decimal _principal;

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

        private double _interestRate;

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "利率")]
        public double InterestRate
        {
            get => _interestRate;
            set
            {
                if (value >= 3 && value <= 16)
                {
                    _interestRate = value / 100;
                }
                else if (value >= 0.03 && value <= 0.16)
                {
                    _interestRate = value;
                }
                else
                {
                    _interestRate = 0.03;
                }
            }
        }

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

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "申貸原因")]
        public string Reason { get; set; }
    }

    public static class LoanAssembler
    {
        public static Loan ViewModelToEntity(this LoanCreateVM viewModel, int borrowerId)
        {
            return new Loan
            {
                BorrowerId = borrowerId,
                Principal = viewModel.Principal,
                InterestRate = viewModel.InterestRate,
                NumOfPeriods = viewModel.NumOfPeriods,
                Reason = viewModel.Reason,
                CreationTime = DateTime.Now
            };
        }

        public static LoanVM EntityToViewModel(this Loan loan)
        {
            return new LoanVM
            {
                Principal = loan.Principal,
                InterestRate = loan.InterestRate,
                NumOfPeriods = loan.NumOfPeriods,
                Reason = loan.Reason,
                CreationTime = loan.CreationTime
            };
        }

        public static List<LoanVM> EntitiesToViewModels(this List<Loan> loans)
        {
            if (loans == null || loans.Count() == 0)
            {
                return Enumerable.Empty<LoanVM>().ToList();
            }

            return loans.Select(l => l.EntityToViewModel()).ToList();
        }
    }
}