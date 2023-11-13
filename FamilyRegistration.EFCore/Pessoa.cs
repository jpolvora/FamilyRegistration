using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamilyRegistration.EFCore;

[Table("Pessoas", Schema = "public")]
[Index(nameof(FullName), IsUnique = true)]
public class Pessoa : EntityBase<Guid>
{
    [StringLength(100), Column("full_name")]
    public string? FullName { get; set; }

    [StringLength(10), Column("gender")]
    public string? Gender { get; set; }

    [Column("age")]
    public int Age { get; set; }

    [Column("income")]
    public decimal Income { get; set; }

    [Column("parent")]
    public Guid? Parent { get; set; }

    [Column("dependents")]
    public int Dependents { get; set; }
}
