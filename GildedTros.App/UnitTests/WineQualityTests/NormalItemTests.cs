using Xunit;

namespace GildedTros.App.UnitTests.WineQualityTests
{
    public class NormalItemTests
    {
        private static Item CreateNormalItem(int sellIn, int quality) => new()
        {
            Name = "Normal",
            SellIn = sellIn,
            Quality = quality
        };



        [Theory]
        [InlineData(10, 20, 9, 19)]
        [InlineData(1, 10, 0, 9)]
        public void NormalItem_Degrades_One_Per_Day(int sellIn, int quality, int expectedSellIn, int expectedQuality)
        {
            var item = CreateNormalItem(sellIn, quality);

            GilderTrosUnitHelper.Update(item);

            Assert.Equal(expectedSellIn, item.SellIn);
            Assert.Equal(expectedQuality, item.Quality);
        }

        [Theory]
        [InlineData(0, 10, 8)]  // expired => -2 quality
        [InlineData(-1, 10, 8)]
        public void NormalItem_Degrades_Twice_When_Expired(int sellIn, int quality, int expectedQuality)
        {
            var item = CreateNormalItem(sellIn, quality);

            GilderTrosUnitHelper.Update(item);

            Assert.Equal(expectedQuality, item.Quality);
        }

        [Theory]
        [InlineData(5, 0)]
        [InlineData(0, 0)]
        public void NormalItem_Quality_Never_Goes_Below_Zero(int sellIn, int quality)
        {
            var item = CreateNormalItem(sellIn, quality);

            GilderTrosUnitHelper.Update(item);

            Assert.True(item.Quality >= 0);
        }
    }
}
