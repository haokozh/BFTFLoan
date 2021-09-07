using BFTFLoan.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.Repositories
{
    public class InvestmentRepository
    {
        private AppDbContext db = new AppDbContext();

        public List<Investment> FindAllInvestmentByInvestorId(int investorId)
        {
            return db.Investment.Where(i => i.InvestorId == investorId).ToList();
        }
    }
}