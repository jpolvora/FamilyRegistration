namespace FamilyRegistration.Core.Adapters;
public class TextAdapter
{
    public TextAdapter(List<string> lines)
    {
        Lines = lines;
    }

    public List<string> Lines { get; }
}
