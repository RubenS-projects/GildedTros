namespace GildedTros.App.ItemQualityServices.ItemQualities
{
    internal class SmellyItemService : ItemQualityServiceBase
    {
        // Smelly Items degrade in quality twice as fast as normal items.
        // This means they decrease in quality by 2 each day before the sell-in date, and by 4 each day after the sell-in date has passed.
        public override void UpdateQuality(Item item)
        {
            var amount = IsExpired(item) ? 4 : 2;
            Decrease(item, amount);
            DecreaseSellIn(item);

        }
    }
}
