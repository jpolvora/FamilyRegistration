namespace FamilyRegistration.Data;

public class Pessoa
{
    public Guid Id { get; set; }
    public string FullName { get; set; }

    public string Gender { get; set; }

    public int Age { get; set; }

    public decimal Income { get; set; }

    public string Parent { get; set; }

    public int Dependents { get; set; }
}
