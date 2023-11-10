using FamilyRegistration.Core.Datasources;
using FamilyRegistration.Core.Decorator;
using FamilyRegistration.Core.Pipelines;
using FamilyRegistration.Core.Strategy;
using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FamilyRegistration.Web.Routes;

public class RouteHandlers
{
    public static async Task<Ok<OutputItem[]>> HandleGet(IDataSource dataSource, IProcessDataUseCase useCase, int count = 100)
    {
        //pegar dados de algum lugar já formatados
        var data = await dataSource.GetData(1, count);

        //prepara o input par ao UseCase
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

    public static async Task<Ok<OutputItem[]>> HandleJsonPost(JsonFormatOne[] requestData, IProcessDataUseCase useCase)
    {
        //convert

        //pegar dados de algum lugar
        var input = requestData.Select(s => s.Adapt()).AsInput();

        //instanciar useCase e executar
        //o useCase fica responsável por coordenar as adaptações entre input e output da pipeline

        var output = await useCase.Execute(input);

        //ordenar o output pelo Score mais alto
        var result = output.OrderByDescending(x => x.Score).ToArray();

        return TypedResults.Ok(result);
    }

    public static async Task<Ok<OutputItem[]>> HandleGetWithDecoratorStrattegy(IDataSource dataSource, int count = 100)
    {
        //using var scope = provider.CreateScope();
        //var dataSource = scope.ServiceProvider.GetRequiredService<IDataSource>();
        //pegar dados de algum lugar já formatados
        var data = await dataSource.GetData(1, count);

        //prepara o input par ao UseCase
        var input = new Input(data);

        //instanciar useCase e executar
        //o useCase fica responsável por coordenar as adaptações entre input e output da pipeline
        IProcessDataStrategy strategy = new ProcessDataWithDecorator(new AggregateScoreCalculator());
        IProcessDataUseCase useCase = new ProcessDataUseCase(strategy);
        var output = await useCase.Execute(input);

        //ordenar o output pelo Score mais alto
        var result = output.OrderByDescending(x => x.Score).ToArray();

        return TypedResults.Ok(result);
    }

    public static async Task<Ok<OutputItem[]>> HandleGetWithPipelineStrategy(IDataSource dataSource, int count = 100)
    {
        //using var scope = provider.CreateScope();
        //var dataSource = scope.ServiceProvider.GetRequiredService<IDataSource>();
        //pegar dados de algum lugar já formatados
        var data = await dataSource.GetData(1, count);

        //prepara o input par ao UseCase
        var input = new Input(data);

        //instanciar useCase e executar
        //o useCase fica responsável por coordenar as adaptações entre input e output da pipeline
        IProcessDataStrategy strategy = new ProcessDataWithPipeline(new CustomPipeline());
        IProcessDataUseCase useCase = new ProcessDataUseCase(strategy);
        var output = await useCase.Execute(input);

        //ordenar o output pelo Score mais alto
        var result = output.OrderByDescending(x => x.Score).ToArray();

        return TypedResults.Ok(result);
    }

    public static async Task<Ok<OutputItem[]>> HandleGetWithTransactionScriptStrategy(IDataSource dataSource, int count = 100)
    {
        //using var scope = provider.CreateScope();
        //var dataSource = scope.ServiceProvider.GetRequiredService<IDataSource>();
        //pegar dados de algum lugar já formatados
        var data = await dataSource.GetData(1, count);

        //prepara o input par ao UseCase
        var input = new Input(data);

        //instanciar useCase e executar
        //o useCase fica responsável por coordenar as adaptações entre input e output da pipeline
        IProcessDataStrategy strategy = new ProcessDataWithTransactionScript();
        IProcessDataUseCase useCase = new ProcessDataUseCase(strategy);
        var output = await useCase.Execute(input);

        //ordenar o output pelo Score mais alto
        var result = output.OrderByDescending(x => x.Score).ToArray();

        return TypedResults.Ok(result);
    }

    public static async Task<Ok<OutputItem[]>> HandleGetWithObserverStrategy(IDataSource dataSource, int count = 100)
    {
        //pegar dados de algum lugar já formatados
        var data = await dataSource.GetData(1, count);

        //prepara o input par ao UseCase
        var input = new Input(data);

        //instanciar useCase e executar
        //o useCase fica responsável por coordenar as adaptações entre input e output da pipeline
        IProcessDataStrategy strategy = new ProcessDataWithObservers();
        IProcessDataUseCase useCase = new ProcessDataUseCase(strategy);
        var output = await useCase.Execute(input);

        //ordenar o output pelo Score mais alto
        var result = output.OrderByDescending(x => x.Score).ToArray();

        return TypedResults.Ok(result);
    }
}

