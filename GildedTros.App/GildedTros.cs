using GildedTros.App.Constants;
using System;
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
                UpdateItemQuality(item);
            }
        }

        private static void UpdateItemQuality(Item item)
        {
            // Legendary items do not change in quality or sell-in date, since they never have to be sold
            // skipping the rest of the logic for legendary items
            if (IsLegendary(item)) return;

            bool isExpired = item.SellIn <= 0;

            UpdateQuality(item, isExpired);
            UpdateSellIn(item);
        }

        private static void UpdateQuality(Item item, bool isExpired)
        {
            if (IsGoodWine(item))
            {
                IncreaseQuality(item);
                if (isExpired) IncreaseQuality(item);
                return;
            }

            if (IsBackstagePass(item))
            {
                UpdateBackstagePass(item, isExpired);
                return;
            }

            int degradationAmount = IsSmelly(item) ? 2 : 1;

            if (isExpired)
                degradationAmount *= 2;

            DecreaseQuality(item, degradationAmount);
        }

        private static void UpdateBackstagePass(Item item, bool isExpired)
        {
            if (isExpired)
            {
                item.Quality = 0;
                return;
            }

            IncreaseQuality(item);

            if (item.SellIn <= 10)
                IncreaseQuality(item);

            if (item.SellIn <= 5)
                IncreaseQuality(item);
        }

        private static void UpdateSellIn(Item item)
        {
            item.SellIn -= 1;
        }

        private static bool IsLegendary(Item item) => item.Name == WineNames.BDAWG_KEYCHAIN;

        private static bool IsSmelly(Item item) => item.Name is WineNames.DUPLICATE_CODE or WineNames.LONG_METHODS or WineNames.UGLY_VARIABLE_NAMES;

        private static bool IsGoodWine(Item item) => item.Name == WineNames.GOOD_WINE;

        private static bool IsBackstagePass(Item item) => item.Name is WineNames.BACKSTAGE_PASSES_FOR_REFACTOR or WineNames.BACKSTAGE_PASSES_FOR_HAXX;

        private static void IncreaseQuality(Item item, int amount = 1)
        {
            item.Quality = Math.Min(50, item.Quality + amount);
        }

        private static void DecreaseQuality(Item item, int amount = 1)
        {
            item.Quality = Math.Max(0, item.Quality - amount);
        }
    }
}
