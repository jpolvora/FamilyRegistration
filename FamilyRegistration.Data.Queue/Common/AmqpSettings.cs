namespace FamilyRegistration.Data.Queue.Common;

public class AmqpSettings : IAmqpSettings
{
    public const string SectionName = nameof(AmqpSettings);
    public bool? Enabled { get; set; }
    public string? HostName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? VirtualHost { get; set; }
}
