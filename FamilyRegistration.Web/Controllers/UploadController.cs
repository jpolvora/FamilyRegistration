using System.Text.Json;
using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Data;
using Microsoft.AspNetCore.Mvc;

namespace FamilyRegistration.Web.Controllers;
[ApiController()]
[Route("[controller]/[action]")]
public class UploadController : Controller
{
    private readonly IProcessDataUseCase _useCase;

    public UploadController(IProcessDataUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost()]
    public async Task<IResult> JsonFile(IFormFile formFile)
    {
        if (formFile == null || formFile.Length == 0)
        {
            return TypedResults.BadRequest();
        }

        using var fileStream = formFile.OpenReadStream();
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var jsonData = await JsonSerializer.DeserializeAsync<JsonFormatOne[]>(fileStream, serializeOptions)
            ?? throw new Exception("Empty jsonData after deserializing");

        var input = jsonData.Select(s => s.Adapt()).AsInput();

        //o useCase fica responsável por coordenar as adaptações entre input e output da pipeline

        var output = await _useCase.Execute(input);

        //ordenar o output pelo Score mais alto
        var result = output.OrderByDescending(x => x.Score).ToArray();

        return TypedResults.Ok(result);

    }
}

