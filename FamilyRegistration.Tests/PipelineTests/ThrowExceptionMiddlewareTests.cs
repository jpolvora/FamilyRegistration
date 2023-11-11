using FamilyRegistration.Core;
using FamilyRegistration.Core.Pipeline.Middlewares;
using FluentAssertions;

namespace FamilyRegistration.Tests.PipelineTests;

public class ThrowExceptionMiddlewareTests
{
    [Fact]
    public async void ShouldContainErrors()
    {
        var ctx = new FamilyContext()
        {
            Key = Guid.NewGuid().ToString(),
        };

        var sut = new ThrowExceptionMiddleware();

        Func<Task> act = async () => await sut.Execute(ctx);
        await act.Should().ThrowAsync<NotImplementedException>();
    }
}