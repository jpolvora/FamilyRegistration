using FamilyRegistration.Core.UseCases;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FamilyRegistration.Web.Routes;

public class SampleDataRoute
{
    public static async Task<Ok<Report[]>> Handle()
    {
        //pegar dados de algum lugar
        var items = SampleData.Generate(100);
        var input = new ProcessarListaInput(items);


        //instanciar useCase e executar
        //o useCase fica responsável por coordenar as adaptações entre input e output da pipeline
        var useCase = new ProcessarLista();
        var output = await useCase.Execute(input);

        //ordenar o output pelo Score mais alto
        var result = output.Data.OrderByDescending(x => x.Score).ToArray();

        return TypedResults.Ok(result);
    }

}
