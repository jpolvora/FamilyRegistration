using FamilyRegistration.Core.UseCases.ProcessData;

namespace FamilyRegistration.Data;


//{"id":5,"full_name":"Zebadiah Ovanesian","gender":"Male","income":1269.5,"age":41,"dependents":[{"income":136.1,"age":72,"full_name":"Sharl Plaskitt"},{ "income":526.54,"age":61,"full_name":"Gordy Connochie"},{ "income":null,"age":68,"full_name":"Jean Petworth"},{ "income":null,"age":78,"full_name":"Leora Fitchell"},{ "income":null,"age":23,"full_name":"Fraze Merrywether"}]},

public class JsonFormatOne
{
    public required string Id { get; set; }

    public decimal Income { get; set; }


    public List<JsonFormatOneDependent>? Dependents { get; set; }

    public int? TotalDependents { get => this.Dependents?.Count; }


    public InputItem Adapt()
    {
        return new()
        {
            Key = this.Id,
            FamilyIncome = this.Income + (this.Dependents?.Select(s => s.Income).Sum()).GetValueOrDefault(0),
            NumOfDependents = this.TotalDependents.GetValueOrDefault(0),
        };
    }

    public class JsonFormatOneDependent
    {
        public decimal? Income { get; set; }

    }
}
