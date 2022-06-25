using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shared.KafkaServices;
using Shared.KafkaServices.Classes;
using Shared.KafkaServices.Interfaces;
using Shared.Services.Interfaces.Interfaces;

namespace MainProcessingService;
public class Program
{
    public static void Main(string[] args)
    {
        var app = CreateHostBuilder(args).Build();
        Console.WriteLine("Main Processing Service has started..");
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

            services.AddTransient<KafkaConsumer>();
            services.AddTransient<IConsumerProvider, KafkaConsumerProvider>();
            services.AddTransient<IKafkaSchemaProvider, KafkaSchemaProvider>();
            services.AddSingleton<IMainProcessingService, MainProcessingService.Services.Classes.MainProcessingService>();
            services.AddTransient<KafkaConsumerProvider>();
            services.AddTransient<KafkaConfigurationProvider>();
            services.AddHostedService<HostedService>();

            services.AddSingleton((ILogger)new LoggerConfiguration()
                .Enrich.FromLogContext()
                .CreateLogger());

        });
        return hostBuilder;
    }
}