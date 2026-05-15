using Microsoft.AspNetCore.HttpLogging;
using Serilog;

namespace Logging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Built in Loging
            //builder.Host.ConfigureLogging(logging =>
            //{
            //    logging.ClearProviders();
            //    logging.AddConsole();
            //    logging.AddDebug();
            //    logging.AddEventLog();
            //}); 
            #endregion

            // SeriLog 

            builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider service, LoggerConfiguration loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(context.Configuration) // Read configuration from appsettings.json
                    .ReadFrom.Services(service); //read out current app services and make them avilable to serilog
            });

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields =
                    HttpLoggingFields.RequestPropertiesAndHeaders |
                     HttpLoggingFields.ResponsePropertiesAndHeaders;



                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            });
            builder.Services.AddControllersWithViews();
            var app = builder.Build();
            app.UseSerilogRequestLogging();

            //create application pipeline
            if (builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Logger.LogDebug("debug-message");
            //app.Logger.LogInformation("information-message");
            //app.Logger.LogWarning("warning-message");
            //app.Logger.LogError("error-message");
            //app.Logger.LogCritical("critical-message");

            

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();

        }
    }
}
