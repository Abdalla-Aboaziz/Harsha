using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Filters
{
    // implement IActionFilter interface  for Synchronous action filters 
    public class LoggingActionFilter(ILogger<LoggingActionFilter> logger): IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) // This method is called before the action method is executed
        {
            // Code to execute before the action executes
            logger.LogInformation("Executiong action :{ActionName} on Controller {ControllerName} with Arguments {Arguments}",
                context.ActionDescriptor.DisplayName,
                context.Controller,
                context.ActionArguments
                );

            // If I set Result here , the action method will not be executed and the result will be returned immediately(اقدر اعمل بلوك واخليه يرجع من هنا  وميكملش ولا يروح لالكشن حتي )
             context.Result = new UnauthorizedResult();


        }

        public void OnActionExecuted(ActionExecutedContext context) // This method is called after the action method is executed
        {
            // Code to execute after the action executes
            logger.LogInformation("Executed action :{ActionName} on Controller {ControllerName} with StatusCode {StatusCode}",
               context.ActionDescriptor.DisplayName,
               context.Controller,
               context.HttpContext.Response.StatusCode
               );
        }
    }
}
