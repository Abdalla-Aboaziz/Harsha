using Autofac;
using Autofac.Extensions.DependencyInjection;
using Di_Contract;
using DiService;

var builder = WebApplication.CreateBuilder(args);
// Use Autofac as the IOC Container
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); // Enabling Autofac as the IOC Container
// IOC Container
builder.Services.AddControllersWithViews();
// builder.Services.Add(new ServiceDescriptor(typeof(ICiteiesServices), typeof(CitiesServices), ServiceLifetime.Scoped));
builder.Services.AddScoped<ICiteiesServices, CitiesServices>(); // Ioc 
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
   // builder.RegisterType<CitiesServices>().As<ICiteiesServices>().InstancePerDependency(); // Add Transient Service
    builder.RegisterType<CitiesServices>().As<ICiteiesServices>().InstancePerLifetimeScope(); // Add Scoped Service
   // builder.RegisterType<CitiesServices>().As<ICiteiesServices>().SingleInstance(); // Add Singleton Service

});// Autofac Ioc

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();