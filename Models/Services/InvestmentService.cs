using BFTFLoan.Models.EFModels;
using BFTFLoan.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.Services
{
    public class InvestmentService
    {
        private readonly MemberService memberService = new MemberService();
        private readonly InvestmentRepository investmentRepository = new InvestmentRepository();

        public Member FindMemberByAccount(string account)
        {
            return memberService.FindMemberByAccount(account);
        }

        public List<Investment> FindAllInvestmentByInvestorId(int investorId)
        {
            return investmentRepository.FindAllInvestmentByInvestorId(investorId);
        }
    }
}