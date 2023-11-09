using FamilyRegistration.Core.Datasources;
using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FamilyRegistration.Web.Routes;

public class RouteHandlers
{
    public static async Task<Ok<OutputItem[]>> HandleGet(IProcessDataUseCase useCase, int count = 100)
    {
        //pegar dados de algum lugar

        IDataSource dataSource = new SampleDataGenerator(count);
        var data = dataSource.GetData();

        var input = new Input(data);

        //instanciar useCase e executar
        //o useCase fica responsável por coordenar as adaptações entre input e output da pipeline

        var output = await useCase.Execute(input);

        //ordenar o output pelo Score mais alto
        var result = output.OrderByDescending(x => x.Score).ToArray();

        return TypedResults.Ok(result);
    }

    public static async Task<Ok<OutputItem[]>> HandlePost(InputItem[] requestData, IProcessDataUseCase useCase)
    {
        //pegar dados de algum lugar        
        var input = new Input(requestData);


        //instanciar useCase e executar
        //o useCase fica responsável por coordenar as adaptações entre input e output da pipeline

        var output = await useCase.Execute(input);

        //ordenar o output pelo Score mais alto
        var result = output.OrderByDescending(x => x.Score).ToArray();

        return TypedResults.Ok(result);
    }

}
