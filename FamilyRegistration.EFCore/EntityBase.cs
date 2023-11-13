using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyRegistration.EFCore;

public abstract class EntityBase<TKey>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
    public required TKey Id { get; set; }

}