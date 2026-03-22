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
            // Normal items decrease in quality
            if (item.Name != WineNames.GOOD_WINE
                && item.Name != WineNames.BACKSTAGE_PASSES_FOR_REFACTOR
                && item.Name != WineNames.BACKSTAGE_PASSES_FOR_HAXX)
            {

                if (item.Quality > 0 && item.Name != WineNames.BDAWG_KEYCHAIN)
                {
                    item.Quality -= 1;
                }
            }
            else
            {
                // "Good Wine" and "Backstage Passes" increase in quality
                if (item.Quality < 50)
                {
                    item.Quality += 1;
                    // "Backstage Passes" increase in quality as the sell-in date approaches

                    if (IsBackstagePass(item))
                    {
                        if (item.SellIn < 11)
                        {
                            IncreaseQuality(item);
                        }

                        if (item.SellIn < 6)
                        {
                            IncreaseQuality(item);
                        }
                    }
                }
            }

            UpdateSellIn(item);

            if (item.SellIn >= 0) return;
            //start expiration Logic

            if (item.Name != WineNames.GOOD_WINE)
            {
                if (IsBackstagePass(item))
                {
                    // "Backstage Passes" quality drops to 0 after the concert
                    item.Quality = 0;
                    return;
                }

                if (item.Name != WineNames.BDAWG_KEYCHAIN)
                {
                    DecreaseQuality(item);
                }
            }
            else
            {
                IncreaseQuality(item);
            }
        }

        private static void UpdateSellIn(Item item)
        {
            if (item.Name != WineNames.BDAWG_KEYCHAIN)
            {
                item.SellIn -= 1;
            }
        }

        private static bool IsLegendary(Item item) => item.Name == WineNames.BDAWG_KEYCHAIN;

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
