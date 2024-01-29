using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsuranceTest.Data.Entities;

[Table("Company")]
public class Company
{
    [Key] public int Id { get; set; }

    [StringLength(200)] [Unicode(false)] public string Name { get; set; } = null!;

    [StringLength(100)] [Unicode(false)] public string Address1 { get; set; } = null!;

    [StringLength(100)] [Unicode(false)] public string? Address2 { get; set; }

    [StringLength(100)] [Unicode(false)] public string? Address3 { get; set; }

    [StringLength(20)] [Unicode(false)] public string Postcode { get; set; } = null!;

    [StringLength(50)] [Unicode(false)] public string Country { get; set; } = null!;

    public bool Active { get; set; }

    [Column(TypeName = "datetime")] public DateTime InsuranceEndDate { get; set; }

    [InverseProperty("Company")] public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();
}