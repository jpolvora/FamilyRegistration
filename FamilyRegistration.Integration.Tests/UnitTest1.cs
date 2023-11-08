using System.Net;
using System.Net.Http.Json;
using FamilyRegistration.Core.UseCases.ProcessarLista;
using Microsoft.AspNetCore.Http;

namespace FamilyRegistration.Integration.Tests;

public class UnitTest1
{
    [Fact]
    public async void ShouldGetReportUsingPipelineAlgorithm()
    {
        const int count = 55;

        var server = new TestWebApplicationFactory<Program>();
        var client = server.CreateClient();
        var response = await client.GetAsync($"/pipeline?count={count}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ReportRow[]>();

        Assert.NotNull(result);

        Assert.True(result.Length == count);
        //Console.WriteLine(result);

    }

    [Fact]
    public async void ShouldGetReportUsingDecoratorAlgorithm()
    {
        const int count = 55;

        var server = new TestWebApplicationFactory<Program>();
        var client = server.CreateClient();
        var response = await client.GetAsync($"/decorator?count={count}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ReportRow[]>();

        Assert.NotNull(result);

        Assert.True(result.Length == count);
        //Console.WriteLine(result);

    }
}
