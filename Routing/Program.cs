


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.Map("mapget1", async context =>   //  https://localhost:7060/
    {
        await context.Response.WriteAsync("Hello from MapGet!");
    });
    endpoints.Map("mapPOST1", async context =>   //  https://localhost:7060/
    {
        await context.Response.WriteAsync("Hello from MapPost!");
    });
   
});



     

app.Run();
