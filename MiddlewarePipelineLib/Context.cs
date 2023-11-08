namespace MiddlewarePipelineLib;

public abstract class Context
{
    public List<Exception> Errors { get; } = new List<Exception>();

    //cancellation token for parallel/threads io ?

}