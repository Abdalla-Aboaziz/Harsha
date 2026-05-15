using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Filters
{
    public class AuditActionFilter(ILogger<LoggingActionFilter> logger) : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User.Identity?.Name ?? "Anonymous";
            var actionName = context.ActionDescriptor.DisplayName;
            logger.LogInformation("User {User} is executing action {ActionName} on controller {ControllerName}",
                user,
                actionName,
                context.Controller
            );
            //Before the action executes

            await next();
            // After the action executes
           
        }
    }
}
