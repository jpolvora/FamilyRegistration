namespace FamilyRegistration.Core.UseCases;
public interface IUseCase<TInput, TOutput>
{
    Task<TOutput> Execute(TInput input);
}
