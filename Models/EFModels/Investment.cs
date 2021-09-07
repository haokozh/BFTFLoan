namespace BFTFLoan.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Investment")]
    public partial class Investment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Investment()
        {
            Resell = new HashSet<Resell>();
        }

        public int Id { get; set; }

        public int InvestorId { get; set; }

        public int LoanId { get; set; }

        public decimal Amount { get; set; }

        public double IRR { get; set; }

        public DateTime CreationTime { get; set; }

        public virtual Member Member { get; set; }

        public virtual Loan Loan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resell> Resell { get; set; }
    }
}
