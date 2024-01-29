using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsuranceTest.Data.Entities;

public class Claim
{
    [Key]
    [Column("UCR")]
    [StringLength(20)]
    [Unicode(false)]
    public string Ucr { get; set; } = null!;

    public int CompanyId { get; set; }

    [Column(TypeName = "datetime")] public DateTime ClaimDate { get; set; }

    [Column(TypeName = "datetime")] public DateTime LossDate { get; set; }

    [Column("Assured Name")]
    [StringLength(100)]
    [Unicode(false)]
    public string AssuredName { get; set; } = null!;

    [Column("Incurred Loss", TypeName = "decimal(15, 2)")]
    public decimal IncurredLoss { get; set; }

    public bool Closed { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("Claims")]
    public virtual Company Company { get; set; } = null!;
}