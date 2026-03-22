using GildedTros.App.Constants;
using Xunit;

namespace GildedTros.App.UnitTests.WineQualityTests
{
    public class LegendaryItemTests
    {
        private static Item CreateLegendaryItem(int sellIn, int quality) => new()
        {
            Name = WineNames.BDAWG_KEYCHAIN,
            SellIn = sellIn,
            Quality = quality
        };



        [Theory]
        [InlineData(10, 80)]
        [InlineData(0, 80)]
        [InlineData(-5, 80)]
        public void LegendaryItem_Never_Changes_SellIn_Or_Quality(int sellIn, int quality)
        {
            Item item = CreateLegendaryItem(sellIn, quality);

            GilderTrosUnitHelper.Update(item);

            Assert.Equal(sellIn, item.SellIn);
            Assert.Equal(quality, item.Quality);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(80)]
        [InlineData(120)] // even invalid starting state stays unchanged
        public void LegendaryItem_Is_Fully_Immutable(int quality)
        {
            Item item = CreateLegendaryItem(10, quality);

            GilderTrosUnitHelper.Update(item);

            Assert.Equal(10, item.SellIn);
            Assert.Equal(quality, item.Quality);
        }

        [Fact]
        public void LegendaryItem_Multiple_Updates_Do_Not_Change_State()
        {
            Item item = CreateLegendaryItem(15, 80);
            GilderTrosUnitHelper.Update(item, 10); // simulate 10 days
            Assert.Equal(15, item.SellIn);
            Assert.Equal(80, item.Quality);
        }
    }
}
