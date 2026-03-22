using GildedTros.App.Constants;
using System.Collections.Generic;
using Xunit;

namespace GildedTros.App.UnitTests.WineQualityTests
{
    public class BackstagePassTests
    {
        private static Item CreateBackstagePass(int sellIn, int quality) => new Item
        {
            Name = WineNames.BACKSTAGE_PASSES_FOR_REFACTOR,
            SellIn = sellIn,
            Quality = quality
        };

        private static void Update(Item item)
        {
            var app = new GildedTros(new List<Item> { item });
            app.UpdateQuality();
        }

        [Theory]
        [InlineData(20, 10, 11)] // +1
        [InlineData(10, 10, 12)] // +2
        [InlineData(5, 10, 13)]  // +3
        public void BackstagePass_Increases_With_Time(int sellIn, int quality, int expected)
        {
            var item = CreateBackstagePass(sellIn, quality);

            Update(item);

            Assert.Equal(expected, item.Quality);
        }

        [Fact]
        public void BackstagePass_Drops_To_Zero_When_Expired()
        {
            var item = CreateBackstagePass(0, 20);

            Update(item);

            Assert.Equal(0, item.Quality);
        }
    }
}
