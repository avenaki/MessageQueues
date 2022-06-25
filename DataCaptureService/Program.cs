using DataCaptureService.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shared.KafkaServices;
using Shared.KafkaServices.Classes;
using Shared.KafkaServices.Interfaces;


namespace DataCaptureService;

public class Program
{
    public static void Main(string[] args)
    {
        var app = CreateHostBuilder(args).Build();
        Console.WriteLine("Data Capture Service has started..");
        Console.WriteLine("Press ctrl c to exit..");
        app.Run();

    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var hostBuilder = new HostBuilder();
        hostBuilder.ConfigureServices(services =>
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

            services.AddOptions<KafkaOptions>()
                .Bind(config.GetSection(KafkaOptions.ConfigurationSection));


            services.AddTransient<IDataCaptureService, DataCaptureService.Services.Classes.DataCaptureService>();
            services.AddTransient<IKafkaProducer,KafkaProducer>();
            services.AddTransient<IProducerProvider, KafkaProducerProvider>();
            services.AddTransient<IKafkaSchemaProvider, KafkaSchemaProvider>();
            services.AddTransient<KafkaConfigurationProvider>();


            services.AddSingleton((ILogger)new LoggerConfiguration()
                .Enrich.FromLogContext()
                .CreateLogger());

        })
        .ConfigureServices((_, services) => services.AddHostedService<HostedService>());
        return hostBuilder;
    }
}