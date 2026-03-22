using GildedTros.App.Constants;
using System.Collections.Generic;
using Xunit;

namespace GildedTros.App.UnitTests.WineQualityTests
{
    public class GoodWineTests
    {
        private static Item CreateGoodWine(int sellIn, int quality) => new()
        {
            Name = WineNames.GOOD_WINE,
            SellIn = sellIn,
            Quality = quality
        };

        private static void Update(Item item)
        {
            var app = new GildedTros(new List<Item> { item });
            app.UpdateQuality();
        }

        [Theory]
        [InlineData(10, 10, 11)]
        [InlineData(5, 49, 50)] // cap at 50
        public void GoodWine_Increases_Normal(int sellIn, int quality, int expected)
        {
            var item = CreateGoodWine(sellIn, quality);

            Update(item);

            Assert.Equal(expected, item.Quality);
        }

        [Theory]
        [InlineData(0, 10, 12)] // expired => +2
        [InlineData(-1, 10, 12)]
        public void GoodWine_Increases_Faster_When_Expired(int sellIn, int quality, int expected)
        {
            var item = CreateGoodWine(sellIn, quality);

            Update(item);

            Assert.Equal(expected, item.Quality);
        }
    }
}
