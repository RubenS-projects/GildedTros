using GildedTros.App.Constants;
using System.Collections.Generic;
using Xunit;

namespace GildedTros.App.UnitTests.WineQualityTests
{
    public class SmellyItemTests
    {
        private static Item CreateSmellyItem(int sellIn, int quality) => new()
        {
            Name = WineNames.DUPLICATE_CODE,
            SellIn = sellIn,
            Quality = quality
        };

        private static void Update(Item item)
        {
            var app = new GildedTros(new List<Item> { item });
            app.UpdateQuality();
        }

        [Theory]
        [InlineData(10, 10, 8)]
        [InlineData(0, 10, 6)] // expired => -4 total
        public void SmellyItems_Degrade_Faster(int sellIn, int quality, int expected)
        {
            var item = CreateSmellyItem(sellIn, quality);

            Update(item);

            Assert.Equal(expected, item.Quality);
        }
    }
}
