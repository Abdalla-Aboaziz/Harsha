using Filters.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews(options =>
{
   // options.Filters.Add<LoggingActionFilter>();
   // options.Filters.Add<AuditActionFilter>();
});
builder.Services.AddScoped<AuditActionFilter>();
builder.Services.AddHttpClient();

var app = builder.Build();



app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.Run();
