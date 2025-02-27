using System;
using System.Threading.Tasks;
using IRON_PROGRAMMER_BOT_Common;
using IRON_PROGRAMMER_BOT_ConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var host = Host.CreateDefaultBuilder(args).ConfigureServices((context, services) =>
            {
                ContainerConfigurator.Configure(context.Configuration, services);
                services.AddHostedService<LongPoolingConfigurator>();
            }).Build();

            await host.RunAsync();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }


}

