using System.Reflection;
using FamilyRegistration.Core.Pipeline.Middlewares;
using MiddlewarePipelineLib;

namespace FamilyRegistration.Core.Pipeline;
public class ScorePipeline : MiddlewarePipeline<FamilyRegistrationContext>
{
    public ScorePipeline()
    {


        UseAllAvailableMiddlewares();
        if (Count == 0)
        {
            Use(new NumOfDependentsMiddleware());
            Use(new FamilyIncomeScoreMiddleware());
            Use(new DummyMiddleware());
            Use(new ThrowExceptionMiddleware());
        }
    }

    private void UseAllAvailableMiddlewares()
    {
        IEnumerable<Type>? types = Assembly.GetEntryAssembly()?.GetReferencedAssemblies().Select(s => s.GetType()).Where(p => typeof(IMiddleware<>).IsAssignableFrom(p));
        if (types != null)
        {
            foreach (Type middleware in types)
            {
                if (middleware == null) continue;

                if (Activator.CreateInstance(middleware.GetType()) is not IMiddleware<FamilyRegistrationContext> instance) continue;

                Use(instance);
            }
        }
    }
}

