using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DiscordBot.Services;
using System.Threading;
using Discord.Interactions;

namespace DiscordBot
{
    public class Program {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _services;
        private readonly DiscordSocketConfig _socketConfig = new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildIntegrations | GatewayIntents.GuildWebhooks | GatewayIntents.GuildMessages,
            AlwaysDownloadUsers = true
        };

        public Program()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("config.json")
                .Build();

            _services = new ServiceCollection()
                .AddSingleton(_configuration)
                .AddSingleton(_socketConfig)
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                .AddSingleton<InteractionHandler>()
                .AddSingleton<WebscrapeService>()
                .BuildServiceProvider();
        }

        static void Main(string[] args) => new Program().RunAsync().GetAwaiter().GetResult();

        public async Task RunAsync()
        {
            var client = _services.GetRequiredService<DiscordSocketClient>();

            client.Log += LogAsync;

            // Here we can initialize the service that will register and execute our commands
            await _services.GetRequiredService<InteractionHandler>().InitializeAsync();

            // Bot token can be provided from the Configuration object we set up earlier
            await client.LoginAsync(TokenType.Bot, _configuration["Token"]);
            await client.StartAsync();

            // Never quit the program until manually forced to.
            await Task.Delay(Timeout.Infinite);
        }

        //public Task TimeAsync()
        //{
        //    var channel = _client.GetChannel(123) as IMessageChannel;
        //    Task.Factory.StartNew(() =>
        //    {
        //        while (true)
        //        {
        //            channel.SendMessageAsync($"{DateTime.UtcNow}");
        //            Thread.Sleep(5000);
        //        }
        //    });
        //    return Task.CompletedTask;
        //}

        private async Task LogAsync(LogMessage message)
           => Console.WriteLine(message.ToString());
    }
}
