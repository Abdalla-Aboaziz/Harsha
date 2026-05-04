using Configurations.OptionPattern;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<MyApiKeyOptions>  // Configure : Load Configuration Data To Object Properties and Register It In DI Container
    (
        builder.Configuration.GetSection("MyApiKey")
    );
// Add Json File To Configuration System
builder.Host.ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
{
    configurationBuilder.AddJsonFile("MyApiKeyJS.json", optional: true, reloadOnChange: true);
});
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.Map("/Config",async context =>
//    {
//        // await context.Response.WriteAsync(app.Configuration["My Key"]);
//       // await context.Response.WriteAsync(app.Configuration.GetValue<string>("My Key", "TEST"));
//       // await context.Response.WriteAsync(app.Configuration.GetValue<string>("xy", "TEST")); // If Key Not Exists


//    }); 
//});
app.MapControllers();
app.Run();