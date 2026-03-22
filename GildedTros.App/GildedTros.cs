using GildedTros.App.ItemQualityServices;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace GildedTros.App
{
    public class GildedTros
    {
        private readonly IList<Item> _items;
        private readonly ILogger<GildedTros> _logger;
        public GildedTros(IList<Item> Items, ILogger<GildedTros> logger)
        {
            _items = Items;
            _logger = logger;

        }


        public void SimulateDays(int days)
        {
            _logger.LogInformation("OMGHAI!");
            for (int i = 0; i < days; i++)
            {
                _logger.LogInformation("-------- day {Day} --------", i);
                _logger.LogInformation("name, sellIn, quality");
                UpdateQuality();
                _logger.LogInformation("");
            }
        }

        private void UpdateQuality()
        {
            foreach (var item in _items)
            {
                _logger.LogInformation("item: { ItemName}, SellIn: { SellIn}, Quality: { Quality}", item.Name, item.SellIn, item.Quality);
                IItemQualityService service = ItemQualityServiceRegistry.Get(item);
                service.UpdateQuality(item);

            }
        }
    }
}
