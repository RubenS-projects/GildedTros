using GildedTros.App.Constants;
using GildedTros.App.ItemQualityServices.ItemQualities;
using System.Collections.Generic;

namespace GildedTros.App.ItemQualityServices
{
    // This registry maps item names to their corresponding quality update services.
    // If an item name is not found in the map, it defaults to using the NormalItemService.
    // adding new item types and their corresponding services is as simple as adding a new entry to the Map dictionary.
    // no changes are needed in the main update loop or elsewhere in the codebase


    //In the future we could change this to DI to make it more flexible and testable.
    //Right now using DI would be overkill for this simple mapping.

    internal static class ItemQualityServiceRegistry
    {
        private static readonly Dictionary<string, IItemQualityService> Map = new()
        {
            [WineNames.BDAWG_KEYCHAIN] = new LegendaryItemService(),

            [WineNames.GOOD_WINE] = new GoodWineItemService(),

            [WineNames.BACKSTAGE_PASSES_FOR_REFACTOR] = new BackstagePassItemService(),
            [WineNames.BACKSTAGE_PASSES_FOR_HAXX] = new BackstagePassItemService(),

            [WineNames.DUPLICATE_CODE] = new SmellyItemService(),
            [WineNames.LONG_METHODS] = new SmellyItemService(),
            [WineNames.UGLY_VARIABLE_NAMES] = new SmellyItemService()
        };

        public static IItemQualityService Get(Item item)
        {
            if (Map.TryGetValue(item.Name, out var strategy))
                return strategy;

            return new NormalItemService();
        }
    }
}
