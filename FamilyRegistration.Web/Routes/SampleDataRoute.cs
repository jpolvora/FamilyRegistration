using FamilyRegistration.Core.UseCases;
using FamilyRegistration.Core.UseCases.ProcessarLista;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FamilyRegistration.Web.Routes;

public class SampleDataRoute
{
    public static async Task<Ok<ReportRow[]>> Handle(IProcessarListaUseCase useCase, int count = 100)
    {
        //pegar dados de algum lugar

        var items = SampleDataGenerator.Generate(count);
        var input = new ProcessarListaInput(items);


        //instanciar useCase e executar
        //o useCase fica responsável por coordenar as adaptações entre input e output da pipeline

        var output = await useCase.Execute(input);

        //ordenar o output pelo Score mais alto
        var result = output.OrderByDescending(x => x.Score).ToArray();

        return TypedResults.Ok(result);
    }

    public static async Task<Ok<ReportRow[]>> HandlePost(FamilyDTO[] requestData, IProcessarListaUseCase useCase)
    {
        //pegar dados de algum lugar        
        var input = new ProcessarListaInput(requestData);


        //instanciar useCase e executar
        //o useCase fica responsável por coordenar as adaptações entre input e output da pipeline

        var output = await useCase.Execute(input);

        //ordenar o output pelo Score mais alto
        var result = output.OrderByDescending(x => x.Score).ToArray();

        return TypedResults.Ok(result);
    }

}
