using BFTFLoan.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BFTFLoan.Models.Repositories
{
    public class LoanRepository
    {
        private AppDbContext db = new AppDbContext();

        public List<Loan> FindAllLoanByBorrowerId(int borrowerId)
        {
            return db.Loan.Where(l => l.BorrowerId == borrowerId).ToList();
        }
    }
}