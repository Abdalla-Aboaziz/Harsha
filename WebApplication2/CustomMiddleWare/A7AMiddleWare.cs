
using System.Net.Http;

namespace WebApplication2.CustomMiddleWare
{
    public class TESTMiddleWare : IMiddleware
    {
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            await httpContext.Response.WriteAsync("TEST");
            await next(httpContext);
        }
    }
    public static class TESTMiddleWareExtension
    {
        public static IApplicationBuilder UseTESTMiddleWare(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TESTMiddleWare>();
        }
    }



}
