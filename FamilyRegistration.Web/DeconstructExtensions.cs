public static class DeconstructExtensions
{
    public static void Deconstruct(this WebApplicationBuilder builder,
        out IServiceCollection services,
            out ConfigurationManager configuration)
    {
        services = builder.Services;
        configuration = builder.Configuration;
    }
}