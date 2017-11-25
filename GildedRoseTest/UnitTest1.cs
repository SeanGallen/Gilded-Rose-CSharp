using System.Collections.Generic;
using Xunit;
using GildedRose;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        Program app;
        public TestAssemblyTests()
        {


            app = new Program()
            {
                Items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 0},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 0, Quality = 6},
                                              new Item {Name = "Carrots and Peas", SellIn = -10, Quality = 5},
                                          }

            };
        }
        [Fact]
        public void TestTheTruth()
        {
            Assert.Equal("+5 Dexterity Vest", app.Items[0].Name);
        }

        [Fact]
        public void qualityDegradesByOneWhenSellInIsPositive()
        {
            app.UpdateQuality();

            Assert.Equal(19, app.Items[0].Quality);
        }

        [Fact]
        public void qualityDegradesByTwoWhenSellInIsZero()
        {
            app.Items[6].SellIn = 0;
            app.UpdateQuality();

            Assert.Equal(3, app.Items[6].Quality);
        }

        [Fact]
        public void qualityDegradesByTwoWhenSellInIsNegative()
        {
            app.UpdateQuality();

            Assert.Equal(3, app.Items[6].Quality);
        }

        [Fact]
        public void qualityDoesNotDegradeBelowZero()
        {
            app.UpdateQuality();

            Assert.Equal(0, app.Items[2].Quality);
        }

        [Fact]
        public void qualityDoesNotDegradeBelowZeroWhenSellInIsNegative()
        {
            app.UpdateQuality();
            app.UpdateQuality();
            app.UpdateQuality();

            Assert.Equal(0, app.Items[6].Quality);
        }

        [Fact]
        public void qualityOfAgedBrieIncreasesBy1WhenSellInIsPositive()
        {
            app.UpdateQuality();


            Assert.Equal(1, app.Items[1].Quality);
        }

        [Fact]
        public void qualityDoesNotIncreaseAbove50WhenSellInIsNegative()
        {
            app.Items[1].Quality = 50;
            app.Items[1].SellIn = -20;
            app.UpdateQuality();
            app.UpdateQuality();

            Assert.Equal(50, app.Items[1].Quality);
        }

        [Fact]
        public void qualityDoesNotIncreaseAbove50()
        {
            app.Items[1].Quality = 50;
            app.UpdateQuality();
            app.UpdateQuality();

            Assert.Equal(50, app.Items[1].Quality);
        }


        [Fact]
        public void qualityOfAgedBrieIncreasesBy2WhenSellInIsNegative()
        {
            app.Items[1].SellIn = -5;
            app.UpdateQuality();
            app.UpdateQuality();

            Assert.Equal(4, app.Items[1].Quality);
        }

        [Fact]
        public void sellInDecreasesBy1()
        {
            app.UpdateQuality();

            Assert.Equal(9, app.Items[0].SellIn);
        }

        [Fact]
        public void sellInDecreasesBy1EvenWhenNegative()
        {
            app.UpdateQuality();

            Assert.Equal(-11, app.Items[6].SellIn);
        }

        [Fact]
        public void sulfurasNeverChanges()
        {
            app.UpdateQuality();

            Assert.Equal(80, app.Items[3].Quality);
        }

        [Fact]
        public void backstagePassesIncreaseByTwoWhenThereAreTenDaysOrLessButMoreThenFiveDaysLess()
        {
            app.Items[4].SellIn = 8;
            app.UpdateQuality();
            Assert.Equal(22, app.Items[4].Quality);
            app.Items[4].SellIn = 7;

        }

        [Fact]
        public void backstagePassQualityIncreasesByThreeWhenThereAreLessThenFiveDays()
        {
            app.Items[4].SellIn = 3;
            app.UpdateQuality();
            Assert.Equal(23, app.Items[4].Quality);
        }

        [Fact]
        public void backstagePassQualityQualityDropsToZeroAfterTheConcert()
        {
            app.Items[4].SellIn = -1;
            app.UpdateQuality();
            Assert.Equal(0, app.Items[4].Quality);
        }

        [Fact]
        public void backstagePassQualityDoesNotIncreaseAbove50()
        {
            app.Items[4].Quality = 50;
            Assert.Equal(50, app.Items[4].Quality);

            app.UpdateQuality();

            Assert.Equal(50, app.Items[4].Quality);

        }

        //[Fact]
        //public void conjuredStuffDecreasesAndQualityDoesNotDecreaseBelowZero()
        //{
        //    app.Items[5].Quality = 1;
        //    app.UpdateQuality();

        //    Assert.Equal(0, app.Items[5].Quality);
        //}

        //[Fact]
        //public void conjuredStuffDecreasesTwoPerNight()
        //{
        //    app.UpdateQuality();

        //    Assert.Equal(4, app.Items[5].Quality);
        //}

        //[Fact]
        //public void conjuredStuffDecreasesFourWhenSellInIsNegative()
        //{
        //    app.Items[5].SellIn = -3;
        //    app.UpdateQuality();

        //    Assert.Equal(2, app.Items[5].Quality);
        //}


    }
}
