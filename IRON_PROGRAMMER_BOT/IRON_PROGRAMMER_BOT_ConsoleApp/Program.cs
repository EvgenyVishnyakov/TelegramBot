﻿using System.Threading.Tasks;
using IRON_PROGRAMMER_BOT_ConsoleApp;
using IRON_PROGRAMMER_BOT_webhook;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args).ConfigureServices((context, services) =>
        {
            ContainerConfigurator.Configure(context.Configuration, services);

            services.AddHostedService<LongPoolingConfigurator>();

        }).Build();

        await host.RunAsync();
    }
}

