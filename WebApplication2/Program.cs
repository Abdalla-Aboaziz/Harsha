
using WebApplication2.CustomMiddleWare;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TESTMiddleWare>();

var app = builder.Build();
// طالما مش محدد النوع بتاع البارميترات  يبقا لازم ارجع ال نكست 



// app.Use In Short Circuiting Middleware

//app.Use(async (HttpContent httpContent,RequestDelegate next) =>
//{
//    await httpContent.Response.WriteAsync("Hello World!");

//});

app.UseCustomTESTMiddleWarecs();
//app.UseTESTMiddleWare();
app.Run(async httpContent =>
{
    await httpContent.Response.WriteAsync("Hello World 2");
});

            
app.Run();
        
    

