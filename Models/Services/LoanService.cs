using BFTFLoan.Models.EFModels;
using BFTFLoan.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.Services
{
    public class LoanService
    {
        private readonly MemberService memberService = new MemberService();
        private readonly LoanRepository loanRepository = new LoanRepository();

        public Member FindMemberByAccount(string account)
        {
            return memberService.FindMemberByAccount(account);
        }

        public List<Loan> FindAllLoanByBorrowerId(int borrowerId)
        {
            return loanRepository.FindAllLoanByBorrowerId(borrowerId);
        }
    }
}