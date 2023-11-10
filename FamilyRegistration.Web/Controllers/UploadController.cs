using System.Text.Json;
using FamilyRegistration.Core.UseCases.ProcessData;
using FamilyRegistration.Data;
using Microsoft.AspNetCore.Mvc;

namespace FamilyRegistration.Web.Controllers;
[ApiController]
[Route("/upload")]
public class UploadController : Controller
{
    private readonly IProcessDataUseCase _useCase;

    public UploadController(IProcessDataUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost()]
    [Route("/upload-json")]

    public async Task<IResult> Index(IFormFile formFile)
    {
        if (formFile != null && formFile.Length > 0)
        {
            using var fileStream = formFile.OpenReadStream();
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var requestData = await JsonSerializer.DeserializeAsync<JsonFormatOne[]>(fileStream, serializeOptions);

            if (requestData != null)
            {
                var input = requestData.Select(s => s.Adapt()).AsInput();

                //instanciar useCase e executar
                //o useCase fica responsável por coordenar as adaptações entre input e output da pipeline

                var output = await _useCase.Execute(input);

                //ordenar o output pelo Score mais alto
                var result = output.OrderByDescending(x => x.Score).ToArray();

                return TypedResults.Ok(result);
            }
        }
        return TypedResults.Ok();
    }
}
