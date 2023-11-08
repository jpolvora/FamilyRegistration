using FamilyRegistration.Core.Datasources;
using FamilyRegistration.Core.Decorators;
using FamilyRegistration.Core.UseCases;
using FamilyRegistration.Core.UseCases.ProcessarLista;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FamilyRegistration.Web.Routes;

public class DecoratorRouteHandlers
{
    public static async Task<Ok<ReportRow[]>> HandleGet(IProcessarListaStrategyUseCase useCase, int count = 100)
    {
        //pegar dados de algum lugar

        IDataSource dataSource = new SampleDataGenerator(count);
        var data = dataSource.GetData();
        var input = new ProcessarListaInput(data);

        //instanciar useCase e executar
        //o useCase fica responsável por coordenar as adaptações entre input e output da pipeline

        useCase.SetStrategy(new ProcessarListaDecoratorUseCase(new DefaultScoreCalculator()));
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
