using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Threading.Tasks;

namespace DiscordBot.Services
{
    public class WebscrapeService
    {
        public WebscrapeService(IServiceProvider services)
        {

        }

        public async Task<string> SearchPhoneNumberAtWP(string phoneNumber)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var url = $"https://www.white-pages.gr/arithmos/{phoneNumber}";
            var doc = await context.OpenAsync(url);
            var title = doc.QuerySelector("h1");
            var neutral = doc.QuerySelector("a.r3");
            var annoying = doc.QuerySelector("a.r2");
            var dangerous = doc.QuerySelector("a.r1");
            var dangerDegree = doc.QuerySelector("#progress-bar-inner");

            return  $@"
{title.Text()}
{neutral.Text()}
{annoying.Text()}
{dangerous.Text()}
Βαθμός του κινδύνου: {dangerDegree.Text()}%
";
        }
    }
}
