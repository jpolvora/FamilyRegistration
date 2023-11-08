using System.Net;
using System.Net.Http.Json;
using FamilyRegistration.Core.UseCases.ProcessarLista;
using Microsoft.AspNetCore.Http;

namespace FamilyRegistration.Integration.Tests;

public class UnitTest1
{
    [Fact]
    public async void ShouldGetReportWithFakeData()
    {
        const int count = 55;

        var server = new TestWebApplicationFactory<Program>();
        var client = server.CreateClient();
        var response = await client.GetAsync($"/?count={count}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ReportRow[]>();

        Assert.NotNull(result);

        Assert.True(result.Length == count);
        //Console.WriteLine(result);

    }
}
