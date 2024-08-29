using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using VictorKrogh.Data;

namespace Microsoft.AspNetCore;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class UnitOfWorkAttribute : Attribute
{
    private IUnitOfWork? UnitOfWork { get; set; }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        UnitOfWork = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (UnitOfWork != null && context.Exception == null)
        {
            UnitOfWork.Commit();
        }
    }

}
