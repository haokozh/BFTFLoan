namespace BFTFLoan.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Loan")]
    public partial class Loan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Loan()
        {
            Investment = new HashSet<Investment>();
        }

        public int Id { get; set; }

        public int BorrowerId { get; set; }

        public decimal Principal { get; set; }

        public double InterestRate { get; set; }

        public int NumOfPeriods { get; set; }

        [Required]
        public string Reason { get; set; }

        public DateTime CreationTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Investment> Investment { get; set; }

        public virtual Repayment Repayment { get; set; }
    }
}
