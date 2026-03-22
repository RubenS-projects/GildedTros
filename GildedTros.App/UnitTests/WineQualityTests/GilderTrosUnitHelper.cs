using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;

namespace GildedTros.App.UnitTests.WineQualityTests
{
    internal static class GilderTrosUnitHelper
    {
        internal static void Update(Item item, int AmountOfDays = 1)
        {
            var loggerMock = new Mock<ILogger<GildedTros>>();
            var app = new GildedTros(new List<Item> { item }, loggerMock.Object);
            app.SimulateDays(AmountOfDays);
        }
    }
}
