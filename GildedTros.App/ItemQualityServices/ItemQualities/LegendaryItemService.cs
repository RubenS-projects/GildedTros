namespace GildedTros.App.ItemQualityServices.ItemQualities
{
    internal sealed class LegendaryItemService : IItemQualityService
    {
        public void UpdateQuality(Item item)
        {
            // Legendary items do not change in quality or sell-in, so this method intentionally does nothing.
        }
    }
}
