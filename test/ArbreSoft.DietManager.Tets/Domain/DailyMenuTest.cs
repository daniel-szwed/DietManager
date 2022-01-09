using ArbreSoft.DietManager.Domain;
using NUnit.Framework;

namespace ArbreSoft.DietManager.Tets.Domain
{
    public class DailyMenuTest
    {
        private NutritionFact milk;
        private NutritionFact oatFlakes;
        private NutritionFact tuna;
        private NutritionFact couscous;

        [SetUp]
        public void Setup()
        {
            milk = new NutritionFact()
            {
                Name = "Milk",
                KiloCalories = 44,
                Proteins = 3,
                TotalCarbohydreates = 4.7m,
                Sugars = 4.7m,
                TotalFats = 1.5m,
                SaturatedFats = 1
            };

            oatFlakes = new NutritionFact()
            {
                Name = "Oat flakes",
                KiloCalories = 372,
                Proteins = 13,
                TotalCarbohydreates = 59,
                Sugars = 0.8m,
                TotalFats = 7.1m,
                SaturatedFats = 1.2m
            };

            tuna = new NutritionFact()
            {
                Name = "Tuna",
                KiloCalories = 75,
                Proteins = 17,
                TotalCarbohydreates = 0.5m,
                Sugars = 0,
                TotalFats = 0.5m,
                SaturatedFats = 0
            };

            couscous = new NutritionFact()
            {
                Name = "Couscous",
                KiloCalories = 376,
                Proteins = 13,
                TotalCarbohydreates = 77,
                Sugars = 0.5m,
                TotalFats = 0.6m,
                SaturatedFats = 0.1m
            };
        }

        [Test]
        public void Sum_CalculateSum_SumOfNutritionFacts()
        {
            // arrange
            var ingredient1 = new Ingredient(milk, 200);
            var ingredient2 = new Ingredient(oatFlakes, 70);
            var ingredient3 = new Ingredient(tuna, 130);
            var ingredient4 = new Ingredient(couscous, 125);

            var meal1 = new Meal();
            var meal2 = new Meal();

            meal1.Childrens.Add(ingredient1);
            meal1.Childrens.Add(ingredient2);
            meal2.Childrens.Add(ingredient3);
            meal2.Childrens.Add(ingredient4);

            var dailyMenu = new DailyMenu();
            dailyMenu.Childrens.Add(meal1);
            dailyMenu.Childrens.Add(meal2);

            // act
            var sum = dailyMenu.Sum().ToFixed(1);

            // assert
            Assert.AreEqual(sum.KiloCalories, 915.9);
            Assert.AreEqual(sum.Proteins, 53.5);
            Assert.AreEqual(sum.TotalCarbohydreates, 147.6);
            Assert.AreEqual(sum.Sugars, 10.6);
            Assert.AreEqual(sum.TotalFats, 9.4);
            Assert.AreEqual(sum.SaturatedFats, 3);
        }
    }
}