using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
namespace GildedTros.App
{
    class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();

            // Logging setup
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Information);
            });

            // App registration
            services.AddSingleton<IList<Item>>(new List<Item>{
                new() {Name = "Ring of Cleansening Code", SellIn = 10, Quality = 20},
                new() {Name = "Good Wine", SellIn = 2, Quality = 0},
                new() {Name = "Elixir of the SOLID", SellIn = 5, Quality = 7},
                new() {Name = "B-DAWG Keychain", SellIn = 0, Quality = 80},
                new() {Name = "B-DAWG Keychain", SellIn = -1, Quality = 80},
                new() {Name = "Backstage passes for Re:factor", SellIn = 15, Quality = 20},
                new() {Name = "Backstage passes for Re:factor", SellIn = 10, Quality = 49},
                new() {Name = "Backstage passes for HAXX", SellIn = 5, Quality = 49},
                // these smelly items do not work properly yet
                new() {Name = "Duplicate Code", SellIn = 3, Quality = 6},
                new() {Name = "Long Methods", SellIn = 3, Quality = 6},
                new() {Name = "Ugly Variable Names", SellIn = 3, Quality = 6}
            });

            services.AddTransient<GildedTros>();

            var provider = services.BuildServiceProvider();

            var app = provider.GetRequiredService<GildedTros>();
            app.SimulateDays(31);

        }
    }
}
