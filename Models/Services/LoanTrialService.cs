using BFTFLoan.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.Services
{
    public class LoanTrialService
    {

        public List<LoanTrialVM> GetLoanTrialResult(LoanTrialCreateVM viewModel)
        {
            List<LoanTrialVM> loanTrialResults = new List<LoanTrialVM>();

            // 第 0 期剩餘本金
            decimal remain = viewModel.Principal;

            // 月利率
            double monthlyInterestRate =
                GetMonthlyInterestRate(viewModel.AnnualRate);

            // 本息平均攤還率
            double averageAmortizationRate =
                GetAverageAmortizationRate(monthlyInterestRate, viewModel.NumOfPeriods);

            // 當期應付款
            decimal currentPayable = Decimal.Round(viewModel.Principal * (decimal)averageAmortizationRate, 0, MidpointRounding.AwayFromZero);

            // 將每一期的試算結果加入 loanTrialResults 中
            for (int currentNumOfPeriods = 1; currentNumOfPeriods <= viewModel.NumOfPeriods; currentNumOfPeriods++)
            {
                // 當期應付利息
                decimal currentInterestPayable = Decimal.Round(remain * (decimal)monthlyInterestRate, 0, MidpointRounding.AwayFromZero);

                // 當期應付本金
                decimal currentPrincipalPayable = Decimal.Round(currentPayable - currentInterestPayable, 0, MidpointRounding.AwayFromZero);

                // 剩餘本金
                remain = Decimal.Round(remain - currentPrincipalPayable, 0, MidpointRounding.AwayFromZero); // 第 0 期之後


                if (currentNumOfPeriods == viewModel.NumOfPeriods)
                {
                    // 因為 剩餘本金 可能不等於 0
                    // 所以將 剩餘本金 加進 當期應付本金 中
                    currentPrincipalPayable += remain;

                    // 當期應付款 = 當期應付本金 + 當期應付利息
                    currentPayable = currentPrincipalPayable + currentInterestPayable;

                    // 剩餘本金歸 0
                    remain = 0;
                }

                // 建立 LoanTrialViewModel 物件 並加入 loanTrialResults 中
                loanTrialResults.Add(new LoanTrialVM
                {
                    CurrentNumOfPeriods = currentNumOfPeriods,
                    CurrentInterestPayable = currentInterestPayable,
                    CurrentPrincipalPayable = currentPrincipalPayable,
                    CurrentPayable = currentPayable,
                    RemainPrincipal = remain
                });
            }

            return loanTrialResults;
        }

        // 計算本息平均攤還率
        // 公式 => {[(1 + 月利率) ^ 期數] * 月利率} / {[(1 + 月利率) ^ 期數] - 1}
        private double GetAverageAmortizationRate(double monthlyInterestRate, int numOfPeriods)
        {
            double powerResult = Math.Pow((1 + monthlyInterestRate), numOfPeriods);

            return (powerResult * monthlyInterestRate) / (powerResult - 1);
        }

        // 計算月利率
        private double GetMonthlyInterestRate(double annualRate)
        {
            return annualRate / 12;
        }
    }
}