using System.Net;
using System.Net.Http.Json;
using FamilyRegistration.Core.UseCases.ProcessData;
using Microsoft.AspNetCore.Http;

namespace FamilyRegistration.Integration.Tests;

public class WebApiTests
{
    [Theory]
    [InlineData(55)]
    [InlineData(100)]
    [InlineData(10)]
    public async void ShouldGetReportWithExactNumberOfRowCount(int count)
    {
        var server = new TestWebApplicationFactory<Program>();
        var client = server.CreateClient();
        var response = await client.GetAsync($"/?count={count}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<OutputItem[]>();

        Assert.NotNull(result);

        Assert.True(result.Length == count);
        //Console.WriteLine(result);
    }
    [Fact]
    public async void ShouldPostAndReturnOutputResult()
    {
        var server = new TestWebApplicationFactory<Program>();
        var client = server.CreateClient();

        var inputItem = new InputItem()
        {
            Key = Guid.NewGuid().ToString(),
            FamilyIncome = 1000,
            NumOfDependents = 3
        };

        var input = new Input(new[] { inputItem });


        var response = await client.PostAsJsonAsync("/", input);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<OutputItem[]>();

        Assert.NotNull(result);

        Assert.True(result.Length == 1);

        Assert.True(result[0].Score == 6);
        //Console.WriteLine(result);
    }
}
