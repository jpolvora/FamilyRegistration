using FamilyRegistration.Core.Factory;
using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Data;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Start");

        ExecuteAsync().Wait();

        Console.WriteLine("End");
    }

    static async Task ExecuteAsync()
    {
        var jsonData = new List<JsonFormatOne>
        {
            new JsonFormatOne()
            {
                Id = Guid.NewGuid().ToString()
            }
        };

        IEnumerable<ProcessDataInputItem> inputItem = jsonData.Select(s => s.Adapt());

        var input = new ProcessDataInput(inputItem);

        var useCase = ProcessData.WithObservers();

        var output = await useCase.Execute(input);


        Console.WriteLine(output);
    }
}