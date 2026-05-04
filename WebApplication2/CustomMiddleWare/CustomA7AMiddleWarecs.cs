using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebApplication2.CustomMiddleWare;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class CustomTESTMiddleWarecs
{
    private readonly RequestDelegate _next;

    public CustomTESTMiddleWarecs(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        await httpContext.Response.WriteAsync("TEST");

        await _next(httpContext);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class CustomTESTMiddleWarecsExtensions
{
    public static IApplicationBuilder UseCustomTESTMiddleWarecs(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomTESTMiddleWarecs>();
    }
}
