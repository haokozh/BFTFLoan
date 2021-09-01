namespace BFTFLoan.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Borrower")]
    public partial class Borrower
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Borrower()
        {
            Loan = new HashSet<Loan>();
        }

        public int Id { get; set; }

        public int MemberId { get; set; }

        public int SchoolId { get; set; }

        [Required]
        [StringLength(10)]
        public string CreditRating { get; set; }

        public virtual Member Member { get; set; }

        public virtual School School { get; set; }

        public virtual Certificate Certificate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Loan> Loan { get; set; }
    }
}
