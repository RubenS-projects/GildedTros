using System;

namespace GildedTros.App.ItemQualityServices
{
    internal abstract class ItemQualityServiceBase : IItemQualityService
    {
        protected const int MaxQuality = 50;
        protected const int MinQuality = 0;

        public abstract void UpdateQuality(Item item);


        protected static bool IsExpired(Item item) => item.SellIn <= 0;

        protected static void Increase(Item item, int amount = 1)
        {
            item.Quality = Math.Min(MaxQuality, item.Quality + amount);
        }

        protected static void Decrease(Item item, int amount = 1)
        {
            item.Quality = Math.Max(MinQuality, item.Quality - amount);
        }

        protected static void DecreaseSellIn(Item item)
        {
            item.SellIn--;
        }

    }
}
