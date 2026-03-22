namespace GildedTros.App.ItemQualityServices.ItemQualities
{
    internal class BackstagePassItemService : ItemQualityServiceBase
    {
        private const int FirstThreshold = 10;
        private const int SecondThreshold = 5;

        // Backstage Passes increase in quality as their sell-in date approaches:
        // - Quality increases by 1 when there are more than 10 days left.
        // - Quality increases by 2 when there are 10 days or less left.
        // - Quality increases by 3 when there are 5 days or less left.
        // - Quality drops to 0 after the concert (sell-in date has passed).
        // The quality of an item is never more than 50.

        public override void UpdateQuality(Item item)
        {
            int increaseAmount = 1;
            if (item.SellIn <= FirstThreshold) increaseAmount++;
            if (item.SellIn <= SecondThreshold) increaseAmount++;
            Increase(item, increaseAmount);

            if (IsExpired(item))
            {
                item.Quality = 0;
            }
            DecreaseSellIn(item);



        }
    }
}
