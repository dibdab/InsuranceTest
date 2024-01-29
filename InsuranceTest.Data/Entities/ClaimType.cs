using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsuranceTest.Data.Entities;

[Table("ClaimType")]
public class ClaimType
{
    [Key] public int Id { get; set; }

    [StringLength(20)] [Unicode(false)] public string Name { get; set; } = null!;
}