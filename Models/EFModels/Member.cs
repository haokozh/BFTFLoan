namespace BFTFLoan.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Member")]
    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            Investment = new HashSet<Investment>();
            Resell = new HashSet<Resell>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Account { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(10)]
        public string IDNumber { get; set; }

        [Required]
        [StringLength(254)]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        public string CellPhone { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        public DateTime CreationTime { get; set; }

        public bool IsEmailVerified { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public DateTime LastLoginTime { get; set; }

        public virtual Borrower Borrower { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Investment> Investment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resell> Resell { get; set; }
    }
}
