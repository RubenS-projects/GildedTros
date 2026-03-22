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
            UpdateQualityDaily(item);

            UpdateSellIn(item);

            if (item.SellIn >= 0) return;
            //start expiration Logic

            HandleExpiredItems(item);
        }

        private static void UpdateQualityDaily(Item item)
        {
            // Normal items decrease in quality
            if (IsLegendary(item))
            {
                // Legendary items do not change in quality
                return;
            }

            if (IsGoodWine(item) || IsBackstagePass(item))
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
                return;
            }
            DecreaseQuality(item);
            if (IsSmelly(item))
            {
                DecreaseQuality(item);
            }
        }

        private static void HandleExpiredItems(Item item)
        {
            if (IsBackstagePass(item))
            {
                // "Backstage Passes" quality drops to 0 after the concert
                item.Quality = 0;
            }
            else if (IsGoodWine(item))
            {
                IncreaseQuality(item);
            }
            else
            {
                // legendary items do not degrade in quality
                if (IsLegendary(item))
                {
                    return;
                }
                // Normal items degrade in quality twice as fast after the sell-in date
                DecreaseQuality(item);
                // Smelly items degrade in quality twice as fast as normal items
                if (IsSmelly(item))
                {
                    DecreaseQuality(item);
                }
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
