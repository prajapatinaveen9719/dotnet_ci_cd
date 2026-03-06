
using Prometheus;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace MonitoringStack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddOpenTelemetry()
                .WithMetrics(metrics =>
                {
                    metrics.AddAspNetCoreInstrumentation();
                    metrics.AddRuntimeInstrumentation();
                    metrics.AddProcessInstrumentation();
                    metrics.AddPrometheusExporter();
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseOpenTelemetryPrometheusScrapingEndpoint();

            app.UseHttpMetrics();   // request metrics 

            app.MapMetrics();       // /metrics endpoint banayega 


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
