using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Network;

namespace LogStashExemple.API
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((context, configuration) =>
                    {
                        configuration.AddEnvironmentVariables();
                    })
                    .UseSerilog((hostingContext, cfg) =>
                    {
                        cfg
                        //.MinimumLevel.Information()
                        .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Error)
                        .Enrich.FromLogContext()
                        // .WriteTo.Http("http://logstash:5043")
                        .WriteTo.TCPSink("tcp://logstash", 5044)
                        .WriteTo.Console(new ElasticsearchJsonFormatter(renderMessage: true));

                        ////cfg.Enrich.FromLogContext();
                        //cfg.Enrich.WithExceptionDetails();
                        //cfg.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Error);

                        //cfg.Enrich.WithAssemblyName();
                        //cfg.Enrich.WithMachineName();
                        //cfg.WriteTo.Console(new JsonFormatter());
                        //cfg.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(hostingContext.Configuration["ElasticConfiguration:Uri"]))
                        //{
                        //    CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                        //    FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                        //    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog | EmitEventFailureHandling.WriteToFailureSink | EmitEventFailureHandling.RaiseCallback,
                        //    IndexFormat = "tests-{0:yyyy.MM.dd-hh.mm}"
                        //});
                    })
                    .UseStartup<Startup>();
                });
    }
}