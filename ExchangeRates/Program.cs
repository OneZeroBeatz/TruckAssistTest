using System;
using System.Threading.Tasks;
using ExchangeRates.Clients;
using ExchangeRates.Models.Configuration;
using ExchangeRates.Repositories;
using ExchangeRates.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExchangeRates
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                          .AddJsonFile("appsettings.json", true, true)
                          .Build();

            using IHost host = CreateHostBuilder(args, configuration).Build();


            IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            var openExchangeRateService = provider.GetRequiredService<OpenExchangeRateService>();
            var nbsExchangeRateService = provider.GetRequiredService<NbsExchangeRateService>();

            var currencySymbols = new string[] { "EUR", "GBP" };

            await openExchangeRateService.AddExchangeRatesFor(currencySymbols);
            //await openExchangeRateService.GetExchangeRatesForLastSevenDays(currencySymbols);

            await nbsExchangeRateService.CompareExchangeRates();

            await host.RunAsync();
        }


        static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration)
        {
            var openExchangeRateClientConfiguration = configuration.GetSection("OpenExchangeRateClient").Get<ExchangeRateClient>();
            var nbsResenjeUrl = configuration.GetSection("NbsResenjeClient").GetValue<string>("ApiUrl");

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddTransient<OpenExchangeRateService, OpenExchangeRateService>()
                            .AddTransient<NbsExchangeRateService, NbsExchangeRateService>()
                            .AddTransient(provider => new OpenExchangeRatesClient(openExchangeRateClientConfiguration.ApiKey,
                                                                                  openExchangeRateClientConfiguration.ApiUrl))
                            .AddTransient(provider => new NbsResenjeClient(nbsResenjeUrl))
                            .AddSingleton<IExchangeRateRepository, ExchangeRateRepository>());

        }
    }
}
