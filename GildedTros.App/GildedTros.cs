using GildedTros.App.ItemQualityServices;
using System.Collections.Generic;

namespace GildedTros.App
{
    public class GildedTros
    {
        readonly IList<Item> Items;
        public GildedTros(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                IItemQualityService service = ItemQualityServiceRegistry.Get(item);
                service.UpdateQuality(item);
            }
        }
    }
}
