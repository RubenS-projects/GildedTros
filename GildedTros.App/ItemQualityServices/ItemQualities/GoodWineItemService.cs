namespace GildedTros.App.ItemQualityServices.ItemQualities
{
    internal sealed class GoodWineItemService : ItemQualityServiceBase
    {
        // Good Wine increases in quality as it ages.
        // Its quality increases by 1 each day before the sell-in date, and by 2 each day after the sell-in date has passed.
        // The quality of an item is never more than 50.
        public override void UpdateQuality(Item item)
        {

            Increase(item);

            if (IsExpired(item))
                Increase(item);

            DecreaseSellIn(item);

        }
    }
}
