namespace GildedTros.App.ItemQualityServices.ItemQualities
{
    internal class NormalItemService : ItemQualityServiceBase
    {
        // decrease quality by 1 before sell date, and by 2 after sell date

        public override void UpdateQuality(Item item)
        {
            var amount = IsExpired(item) ? 2 : 1;
            Decrease(item, amount);
            DecreaseSellIn(item);

        }
    }
}
