namespace FamilyRegistration.Data.Queue.Common;

public interface IAmqpSettings
{
    bool? Enabled { get; set; }
    string? HostName { get; set; }
    string? Password { get; set; }
    string? UserName { get; set; }
    string? VirtualHost { get; set; }
}