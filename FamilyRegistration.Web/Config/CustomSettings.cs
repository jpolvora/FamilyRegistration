using System.ComponentModel.DataAnnotations;

namespace FamilyRegistration.Web.Config;

public class CustomSettings
{
    public const string SectionName = nameof(CustomSettings);

    [Required]
    public EStrategy? Strategy { get; set; }
}
