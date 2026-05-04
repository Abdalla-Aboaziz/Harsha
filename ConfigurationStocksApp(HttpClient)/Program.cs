
using ConfigurationStocksApp_HttpClient_.Servicecs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<FinhubService>();
var app = builder.Build();

           

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.Run();
