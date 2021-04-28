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
            var exchangeRateService = provider.GetRequiredService<ExchangeRateService>();

            await exchangeRateService.AddUsdExchangeRatesFor(new string[] { "EUR", "GBP" });

            await host.RunAsync();
        }


        static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration)
        {
            var exchangeRateClientConfiguration = configuration.GetSection("ExchangeRateClient").Get<ExchangeRateClient>();

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddTransient<ExchangeRateService, ExchangeRateService>()
                            .AddTransient(provider => new OpenExchangeRatesClient(exchangeRateClientConfiguration.ApiKey, 
                                                                                  exchangeRateClientConfiguration.ApiUrl))
                            .AddTransient<IExchangeRateRepository, ExchangeRateRepository>());

        }
    }
}
